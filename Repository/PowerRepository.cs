﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using EntityFramework.BulkInsert.Extensions;
using Newtonsoft.Json;
using PowerQualityModel.DataModel;
using SHWDTech.Platform.Utility;

namespace Repository
{
    public class PowerRepository<T> : RepositoryBase, IRepository where T : ModelBase, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public PowerDbContext DbContext { get; set; }

        /// <summary>
        /// 数据库上下文配置信息
        /// </summary>
        public DbContextConfiguration Configuration => DbContext.Configuration;

        /// <summary>
        /// 数据库上下文相关数据库配置项
        /// </summary>
        public Database Database => DbContext.Database;

        /// <summary>
        /// 进行操作的数据实体
        /// </summary>
        protected IQueryable<T> EntitySet { get; set; }

        /// <summary>
        /// 数据检查条件
        /// </summary>
        protected Expression<Func<T, bool>> CheckFunc { get; set; }

        /// <summary>
        /// 创建一个新的数据仓库泛型基类对象
        /// </summary>
        public PowerRepository()
        {
            DbContext = new PowerDbContext();
        }

        public PowerRepository(string connString) : this()
        {
            DbContext = new PowerDbContext(connString);
        }

        public PowerRepository(PowerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void InitEntitySet()
        {
            EntitySet = CheckFunc == null ? DbContext.Set<T>() : DbContext.Set<T>().Where(CheckFunc);
        }

        public virtual IQueryable<T> GetAllModels()
            => EntitySet;

        public virtual IList<T> GetAllModelList()
            => GetAllModels().ToList();

        public virtual IQueryable<T> GetModels(Expression<Func<T, bool>> exp)
            => EntitySet.Where(exp);

        public virtual IList<T> GetModelList(Expression<Func<T, bool>> exp)
            => GetModels(exp).ToList();

        public virtual T GetModel(Expression<Func<T, bool>> exp)
            => EntitySet.SingleOrDefault(exp);

        public virtual T GetModelIncludeById(long guid, List<string> includes)
        {
            var query = includes.Aggregate(EntitySet, (current, include) => current.Include(include));

            return query.SingleOrDefault(obj => obj.Id == guid);
        }

        public virtual T GetModelInclude(Expression<Func<T, bool>> exp, List<string> includes)
        {
            var query = includes.Aggregate(EntitySet, (current, include) => current.Include(include));

            return query.SingleOrDefault(exp);
        }

        public virtual IQueryable<T> GetModelsInclude(Expression<Func<T, bool>> exp, List<string> includes)
            => includes.Aggregate(EntitySet, (current, include) => current.Include(include)).Where(exp);

        public virtual IList<T> GetModelsListInclude(Expression<Func<T, bool>> exp, List<string> includes)
            => includes.Aggregate(EntitySet, (current, include) => current.Include(include)).Where(exp).ToList();

        public virtual T GetModelById(long guid)
            => EntitySet.SingleOrDefault(obj => obj.Id == guid);

        public virtual int GetCount(Expression<Func<T, bool>> exp)
            => exp == null ? EntitySet.Count() : EntitySet.Where(exp).Count();

        /// <summary>
        /// 创建默认数据模型
        /// </summary>
        /// <param name="generateId">是否生成ID</param>
        /// <returns></returns>
        public static T CreateDefaultModel(bool generateId)
        {
            var model = new T
            {
                ModelState = ModelState.Added
            };

            if (generateId)
            {
                model.Id = 0;
            }

            return model;
        }

        public virtual T ParseModel(string jsonString)
        {
            var model = JsonConvert.DeserializeObject<T>(jsonString);
            model.ModelState = ModelState.Added;

            return model;
        }

        /// <summary>
        /// 执行添加或更新
        /// </summary>
        /// <param name="model"></param>
        protected void DoAddOrUpdate(T model)
        {
            CheckModel(model);

            if (model.IsNew)
            {
                DbContext.Set<T>().Add(model);
            }
            else
            {
                DbContext.Set<T>().Attach(model);
                DbContext.Entry(model).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// 执行批量添加或更新
        /// </summary>
        /// <param name="models"></param>
        protected void DoAddOrUpdate(IEnumerable<T> models)
        {
            CheckModel(models);

            foreach (var model in models.Where(model => model.IsNew))
            {
                DbContext.Set<T>().Add(model);
            }
        }

        public virtual void AddOrUpdate(T model)
        {
            DoAddOrUpdate(model);
        }

        public virtual void AddOrUpdate(IEnumerable<T> models)
        {
            DoAddOrUpdate(models);
        }

        public virtual long AddOrUpdateDoCommit(T model)
        {
            DoAddOrUpdate(model);

            return Submit() != 1 ? 0 : model.Id;
        }

        public virtual int AddOrUpdateDoCommit(IEnumerable<T> models)
        {
            DoAddOrUpdate(models);

            return Submit();
        }

        /// <summary>
        /// 执行部分更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        protected void DoPartialUpdate(T model, List<string> propertyNames)
        {
            DbContext.Set<T>().Attach(model);
            var modelType = model.GetType();
            foreach (var propertyName in propertyNames)
            {
                if (!IsPrimitive(modelType.GetProperty(propertyName).PropertyType)) continue;

                DbContext.Entry(model).Property(propertyName).IsModified = true;
            }
        }

        /// <summary>
        /// 批量执行部分更新
        /// </summary>
        /// <param name="models"></param>
        /// <param name="propertyNames"></param>
        protected void DoPartialUpdate(List<T> models, List<string> propertyNames)
        {
            var modelType = models.First().GetType();
            foreach (var propertyName in propertyNames)
            {
                if (!IsPrimitive(modelType.GetProperty(propertyName).PropertyType)) continue;

                foreach (var model in models)
                {
                    DbContext.Entry(model).Property(propertyName).IsModified = true;
                }
            }
        }

        public virtual void PartialUpdate(T model, List<string> propertyNames)
        {
            DoPartialUpdate(model, propertyNames);
        }

        public virtual void PartialUpdate(List<T> models, List<string> propertyNames)
        {
            DoPartialUpdate(models, propertyNames);
        }

        public virtual long PartialUpdateDoCommit(T model, List<string> propertyNames)
        {
            DoPartialUpdate(model, propertyNames);

            return Submit() != 1 ? 0 : model.Id;
        }

        public virtual int PartialUpdateDoCommit(List<T> models, List<string> propertyNames)
        {
            DoPartialUpdate(models, propertyNames);

            return Submit();
        }

        public virtual void BulkInsert(IEnumerable<T> models)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    DbContext.BulkInsert(models);
                }
                catch (Exception ex)
                {
                    LogService.Instance.Debug("", ex);
                    return;
                }
                scope.Complete();
            }
        }

        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="model"></param>
        protected void DoDelete(T model)
        {
            CheckModel(model);

            DbContext.Set<T>().Remove(model);
        }

        /// <summary>
        /// 执行批量删除
        /// </summary>
        /// <param name="models"></param>
        protected void DoDelete(IEnumerable<T> models)
        {
            CheckModel(models);

            foreach (var model in models)
            {
                DbContext.Set<T>().Remove(model);
            }
        }

        public virtual void Delete(T model)
        {
            DoDelete(model);
        }

        public virtual void Delete(IEnumerable<T> models)
        {
            DoDelete(models);
        }

        public virtual bool DeleteDoCommit(T model)
        {
            DoDelete(model);

            return Submit() == 1;
        }

        public virtual int DeleteDoCommit(IEnumerable<T> models)
        {
            DoDelete(models);

            return Submit();
        }

        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive
            || type == typeof(decimal)
            || type == typeof(string)
            || type == typeof(DateTime)
            || type == typeof(Guid)
            || (type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                && type.GetGenericArguments().Any(t => t.IsValueType && IsPrimitive(t)));
        }

        public virtual bool IsExists(T model) => EntitySet.Any(obj => obj.Id == model.Id);

        public virtual bool IsExists(Func<T, bool> exp) => EntitySet.Any(exp);

        /// <summary>
        /// 检查模型是否符合要求
        /// </summary>
        /// <param name="models"></param>
        private void CheckModel(object models)
        {
            if (CheckFunc == null) return;

            if (models == null) throw new ArgumentNullException(nameof(models));

            var checkList = new List<T>();
            var item = models as T;
            if (item != null) checkList.Add(item);

            var items = models as IEnumerable<T>;
            if (items != null) checkList.AddRange(items);

            if (!checkList.Any(CheckFunc.Compile())) throw new ArgumentException("参数不符合要求");
        }

        /// <summary>
        /// 提交更改
        /// </summary>
        /// <returns></returns>
        private int Submit()
            => DbContext.SaveChanges();
    }
}
