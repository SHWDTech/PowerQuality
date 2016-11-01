var powerCharts = [];
var chartsOption = {};

chartsOption.StepLine = function (params) {
    var opt = {
        title: {
            show: false
        },
        tooltip: {
            trigger: 'axis',
            formatter: function (formatParams) {
                return params['category'][formatParams[0]['dataIndex']].format("yyyy-MM-dd hh:mm:ss S")
                + '<br/>' + '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:' + formatParams[0].color + '"></span>' + formatParams[0]['seriesName'] + '：' + formatParams[0].data
                + '<br/>' + '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:' + formatParams[1].color + '"></span>' + formatParams[1]['seriesName'] + '：' + formatParams[0].data
                + '<br/>' + '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:' + formatParams[2].color + '"></span>' + formatParams[2]['seriesName'] + '：' + formatParams[0].data;
            }
        },
        legend: {
            show: false,
            data: ['最大值', '平均值', '最小值']
        },
        grid: {
            top: '0%',
            left: '0%',
            right: '0%',
            bottom: '0%',
            containLabel: true
        },
        toolbox: {
        },
        xAxis: {
            type: 'category',
            data: params['category'],
            axisLabel: {
                show: params['axisLabel'],
                formatter: function (value) {
                    return value.format("hh:mm:ss S");
                }
            }
        },
        yAxis: {
            type: 'value',
            max: params['maxVal'],
            min: params['minVal'],
            splitNumber: 6,
            minInterval: 0.001,
            interval: params['interval'],
            axisLabel: {
                show: true
            }
        },
        dataZoom: {
            type: 'inside',
            yAxisIndex: null,
            filterMode: 'filter',
            zoomLock: true
        },
        series: [
            {
                name: '最大值',
                type: 'line',
                step: 'start',
                xAxisIndex: 0,
                yAxisIndex: 0,
                lineStyle: {
                    normal: {
                        width: 1
                    }
                },
                data: params['max']
            },
            {
                name: '平均值',
                type: 'line',
                step: 'start',
                xAxisIndex: 0,
                yAxisIndex: 0,
                lineStyle: {
                    normal: {
                        width: 1
                    }
                },
                data: params['avg']
            },
            {
                name: '最小值',
                type: 'line',
                step: 'start',
                xAxisIndex: 0,
                yAxisIndex: 0,
                lineStyle: {
                    normal: {
                        width: 1
                    }
                },
                data: params['min']
            }
        ]
    };

    opt['toolbox'] = {
        show: true,
        feature: {
            dataZoom: {
                show: true,
                yAxisIndex: false
            },
            restore: {
                show: true
            }
        }
    };
    opt.grid.top = '25px';
    if (params['index'] !== 0) {
        opt.toolbox.top = -10000;
    }
    return opt;
};

chartsOption.pushData = function () {

};