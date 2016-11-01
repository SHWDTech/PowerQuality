using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerQualityModel.DataModel
{
    public class Harmonic : SystemModel
    {
        [Required]
        public Guid RecordGuid { get; set; }

        [ForeignKey("RecordGuid")]
        public Record Record { get; set; }

        [Required]
        public Guid ActiveValueGuid { get; set; }

        [ForeignKey("ActiveValueGuid")]
        public ActiveValue ActiveValue { get; set; }

        [Required]
        [Index("IX_RecordIndex", IsClustered = true)]
        public int RecordIndex { get; set; }

        [Required]
        public double VoltageAHarmonic0 { get; set; }

        [Required]
        public double VoltageAHarmonic1 { get; set; }

        [Required]
        public double VoltageAHarmonic2 { get; set; }

        [Required]
        public double VoltageAHarmonic3 { get; set; }

        [Required]
        public double VoltageAHarmonic4 { get; set; }

        [Required]
        public double VoltageAHarmonic5 { get; set; }

        [Required]
        public double VoltageAHarmonic6 { get; set; }

        [Required]
        public double VoltageAHarmonic7 { get; set; }

        [Required]
        public double VoltageAHarmonic8 { get; set; }

        [Required]
        public double VoltageAHarmonic9 { get; set; }

        [Required]
        public double VoltageAHarmonic10 { get; set; }

        [Required]
        public double VoltageAHarmonic11 { get; set; }

        [Required]
        public double VoltageAHarmonic12 { get; set; }

        [Required]
        public double VoltageAHarmonic13 { get; set; }

        [Required]
        public double VoltageAHarmonic14 { get; set; }

        [Required]
        public double VoltageAHarmonic15 { get; set; }

        [Required]
        public double VoltageAHarmonic16 { get; set; }

        [Required]
        public double VoltageAHarmonic17 { get; set; }

        [Required]
        public double VoltageAHarmonic18 { get; set; }

        [Required]
        public double VoltageAHarmonic19 { get; set; }

        [Required]
        public double VoltageAHarmonic20 { get; set; }

        [Required]
        public double VoltageAHarmonic21 { get; set; }

        [Required]
        public double VoltageAHarmonic22 { get; set; }

        [Required]
        public double VoltageAHarmonic23 { get; set; }

        [Required]
        public double VoltageAHarmonic24 { get; set; }

        [Required]
        public double VoltageAHarmonic25 { get; set; }

        [Required]
        public double VoltageAHarmonic26 { get; set; }

        [Required]
        public double VoltageAHarmonic27 { get; set; }

        [Required]
        public double VoltageAHarmonic28 { get; set; }

        [Required]
        public double VoltageAHarmonic29 { get; set; }

        [Required]
        public double VoltageAHarmonic30 { get; set; }

        [Required]
        public double VoltageAHarmonic31 { get; set; }

        [Required]
        public double VoltageAHarmonic32 { get; set; }

        [Required]
        public double VoltageAHarmonic33 { get; set; }

        [Required]
        public double VoltageAHarmonic34 { get; set; }

        [Required]
        public double VoltageAHarmonic35 { get; set; }

        [Required]
        public double VoltageAHarmonic36 { get; set; }

        [Required]
        public double VoltageAHarmonic37 { get; set; }

        [Required]
        public double VoltageAHarmonic38 { get; set; }

        [Required]
        public double VoltageAHarmonic39 { get; set; }

        [Required]
        public double VoltageAHarmonic40 { get; set; }

        [Required]
        public double VoltageAHarmonic41 { get; set; }

        [Required]
        public double VoltageAHarmonic42 { get; set; }

        [Required]
        public double VoltageAHarmonic43 { get; set; }

        [Required]
        public double VoltageAHarmonic44 { get; set; }

        [Required]
        public double VoltageAHarmonic45 { get; set; }

        [Required]
        public double VoltageAHarmonic46 { get; set; }

        [Required]
        public double VoltageAHarmonic47 { get; set; }

        [Required]
        public double VoltageAHarmonic48 { get; set; }

        [Required]
        public double VoltageAHarmonic49 { get; set; }

        [Required]
        public double VoltageAHarmonic50 { get; set; }

        [Required]
        public double VoltageBHarmonic0 { get; set; }

        [Required]
        public double VoltageBHarmonic1 { get; set; }

        [Required]
        public double VoltageBHarmonic2 { get; set; }

        [Required]
        public double VoltageBHarmonic3 { get; set; }

        [Required]
        public double VoltageBHarmonic4 { get; set; }

        [Required]
        public double VoltageBHarmonic5 { get; set; }

        [Required]
        public double VoltageBHarmonic6 { get; set; }

        [Required]
        public double VoltageBHarmonic7 { get; set; }

        [Required]
        public double VoltageBHarmonic8 { get; set; }

        [Required]
        public double VoltageBHarmonic9 { get; set; }

        [Required]
        public double VoltageBHarmonic10 { get; set; }

        [Required]
        public double VoltageBHarmonic11 { get; set; }

        [Required]
        public double VoltageBHarmonic12 { get; set; }

        [Required]
        public double VoltageBHarmonic13 { get; set; }

        [Required]
        public double VoltageBHarmonic14 { get; set; }

        [Required]
        public double VoltageBHarmonic15 { get; set; }

        [Required]
        public double VoltageBHarmonic16 { get; set; }

        [Required]
        public double VoltageBHarmonic17 { get; set; }

        [Required]
        public double VoltageBHarmonic18 { get; set; }

        [Required]
        public double VoltageBHarmonic19 { get; set; }

        [Required]
        public double VoltageBHarmonic20 { get; set; }

        [Required]
        public double VoltageBHarmonic21 { get; set; }

        [Required]
        public double VoltageBHarmonic22 { get; set; }

        [Required]
        public double VoltageBHarmonic23 { get; set; }

        [Required]
        public double VoltageBHarmonic24 { get; set; }

        [Required]
        public double VoltageBHarmonic25 { get; set; }

        [Required]
        public double VoltageBHarmonic26 { get; set; }

        [Required]
        public double VoltageBHarmonic27 { get; set; }

        [Required]
        public double VoltageBHarmonic28 { get; set; }

        [Required]
        public double VoltageBHarmonic29 { get; set; }

        [Required]
        public double VoltageBHarmonic30 { get; set; }

        [Required]
        public double VoltageBHarmonic31 { get; set; }

        [Required]
        public double VoltageBHarmonic32 { get; set; }

        [Required]
        public double VoltageBHarmonic33 { get; set; }

        [Required]
        public double VoltageBHarmonic34 { get; set; }

        [Required]
        public double VoltageBHarmonic35 { get; set; }

        [Required]
        public double VoltageBHarmonic36 { get; set; }

        [Required]
        public double VoltageBHarmonic37 { get; set; }

        [Required]
        public double VoltageBHarmonic38 { get; set; }

        [Required]
        public double VoltageBHarmonic39 { get; set; }

        [Required]
        public double VoltageBHarmonic40 { get; set; }

        [Required]
        public double VoltageBHarmonic41 { get; set; }

        [Required]
        public double VoltageBHarmonic42 { get; set; }

        [Required]
        public double VoltageBHarmonic43 { get; set; }

        [Required]
        public double VoltageBHarmonic44 { get; set; }

        [Required]
        public double VoltageBHarmonic45 { get; set; }

        [Required]
        public double VoltageBHarmonic46 { get; set; }

        [Required]
        public double VoltageBHarmonic47 { get; set; }

        [Required]
        public double VoltageBHarmonic48 { get; set; }

        [Required]
        public double VoltageBHarmonic49 { get; set; }

        [Required]
        public double VoltageBHarmonic50 { get; set; }

        [Required]
        public double VoltageCHarmonic0 { get; set; }

        [Required]
        public double VoltageCHarmonic1 { get; set; }

        [Required]
        public double VoltageCHarmonic2 { get; set; }

        [Required]
        public double VoltageCHarmonic3 { get; set; }

        [Required]
        public double VoltageCHarmonic4 { get; set; }

        [Required]
        public double VoltageCHarmonic5 { get; set; }

        [Required]
        public double VoltageCHarmonic6 { get; set; }

        [Required]
        public double VoltageCHarmonic7 { get; set; }

        [Required]
        public double VoltageCHarmonic8 { get; set; }

        [Required]
        public double VoltageCHarmonic9 { get; set; }

        [Required]
        public double VoltageCHarmonic10 { get; set; }

        [Required]
        public double VoltageCHarmonic11 { get; set; }

        [Required]
        public double VoltageCHarmonic12 { get; set; }

        [Required]
        public double VoltageCHarmonic13 { get; set; }

        [Required]
        public double VoltageCHarmonic14 { get; set; }

        [Required]
        public double VoltageCHarmonic15 { get; set; }

        [Required]
        public double VoltageCHarmonic16 { get; set; }

        [Required]
        public double VoltageCHarmonic17 { get; set; }

        [Required]
        public double VoltageCHarmonic18 { get; set; }

        [Required]
        public double VoltageCHarmonic19 { get; set; }

        [Required]
        public double VoltageCHarmonic20 { get; set; }

        [Required]
        public double VoltageCHarmonic21 { get; set; }

        [Required]
        public double VoltageCHarmonic22 { get; set; }

        [Required]
        public double VoltageCHarmonic23 { get; set; }

        [Required]
        public double VoltageCHarmonic24 { get; set; }

        [Required]
        public double VoltageCHarmonic25 { get; set; }

        [Required]
        public double VoltageCHarmonic26 { get; set; }

        [Required]
        public double VoltageCHarmonic27 { get; set; }

        [Required]
        public double VoltageCHarmonic28 { get; set; }

        [Required]
        public double VoltageCHarmonic29 { get; set; }

        [Required]
        public double VoltageCHarmonic30 { get; set; }

        [Required]
        public double VoltageCHarmonic31 { get; set; }

        [Required]
        public double VoltageCHarmonic32 { get; set; }

        [Required]
        public double VoltageCHarmonic33 { get; set; }

        [Required]
        public double VoltageCHarmonic34 { get; set; }

        [Required]
        public double VoltageCHarmonic35 { get; set; }

        [Required]
        public double VoltageCHarmonic36 { get; set; }

        [Required]
        public double VoltageCHarmonic37 { get; set; }

        [Required]
        public double VoltageCHarmonic38 { get; set; }

        [Required]
        public double VoltageCHarmonic39 { get; set; }

        [Required]
        public double VoltageCHarmonic40 { get; set; }

        [Required]
        public double VoltageCHarmonic41 { get; set; }

        [Required]
        public double VoltageCHarmonic42 { get; set; }

        [Required]
        public double VoltageCHarmonic43 { get; set; }

        [Required]
        public double VoltageCHarmonic44 { get; set; }

        [Required]
        public double VoltageCHarmonic45 { get; set; }

        [Required]
        public double VoltageCHarmonic46 { get; set; }

        [Required]
        public double VoltageCHarmonic47 { get; set; }

        [Required]
        public double VoltageCHarmonic48 { get; set; }

        [Required]
        public double VoltageCHarmonic49 { get; set; }

        [Required]
        public double VoltageCHarmonic50 { get; set; }

        [Required]
        public double CurrentAHarmonic0 { get; set; }

        [Required]
        public double CurrentAHarmonic1 { get; set; }

        [Required]
        public double CurrentAHarmonic2 { get; set; }

        [Required]
        public double CurrentAHarmonic3 { get; set; }

        [Required]
        public double CurrentAHarmonic4 { get; set; }

        [Required]
        public double CurrentAHarmonic5 { get; set; }

        [Required]
        public double CurrentAHarmonic6 { get; set; }

        [Required]
        public double CurrentAHarmonic7 { get; set; }

        [Required]
        public double CurrentAHarmonic8 { get; set; }

        [Required]
        public double CurrentAHarmonic9 { get; set; }

        [Required]
        public double CurrentAHarmonic10 { get; set; }

        [Required]
        public double CurrentAHarmonic11 { get; set; }

        [Required]
        public double CurrentAHarmonic12 { get; set; }

        [Required]
        public double CurrentAHarmonic13 { get; set; }

        [Required]
        public double CurrentAHarmonic14 { get; set; }

        [Required]
        public double CurrentAHarmonic15 { get; set; }

        [Required]
        public double CurrentAHarmonic16 { get; set; }

        [Required]
        public double CurrentAHarmonic17 { get; set; }

        [Required]
        public double CurrentAHarmonic18 { get; set; }

        [Required]
        public double CurrentAHarmonic19 { get; set; }

        [Required]
        public double CurrentAHarmonic20 { get; set; }

        [Required]
        public double CurrentAHarmonic21 { get; set; }

        [Required]
        public double CurrentAHarmonic22 { get; set; }

        [Required]
        public double CurrentAHarmonic23 { get; set; }

        [Required]
        public double CurrentAHarmonic24 { get; set; }

        [Required]
        public double CurrentAHarmonic25 { get; set; }

        [Required]
        public double CurrentAHarmonic26 { get; set; }

        [Required]
        public double CurrentAHarmonic27 { get; set; }

        [Required]
        public double CurrentAHarmonic28 { get; set; }

        [Required]
        public double CurrentAHarmonic29 { get; set; }

        [Required]
        public double CurrentAHarmonic30 { get; set; }

        [Required]
        public double CurrentAHarmonic31 { get; set; }

        [Required]
        public double CurrentAHarmonic32 { get; set; }

        [Required]
        public double CurrentAHarmonic33 { get; set; }

        [Required]
        public double CurrentAHarmonic34 { get; set; }

        [Required]
        public double CurrentAHarmonic35 { get; set; }

        [Required]
        public double CurrentAHarmonic36 { get; set; }

        [Required]
        public double CurrentAHarmonic37 { get; set; }

        [Required]
        public double CurrentAHarmonic38 { get; set; }

        [Required]
        public double CurrentAHarmonic39 { get; set; }

        [Required]
        public double CurrentAHarmonic40 { get; set; }

        [Required]
        public double CurrentAHarmonic41 { get; set; }

        [Required]
        public double CurrentAHarmonic42 { get; set; }

        [Required]
        public double CurrentAHarmonic43 { get; set; }

        [Required]
        public double CurrentAHarmonic44 { get; set; }

        [Required]
        public double CurrentAHarmonic45 { get; set; }

        [Required]
        public double CurrentAHarmonic46 { get; set; }

        [Required]
        public double CurrentAHarmonic47 { get; set; }

        [Required]
        public double CurrentAHarmonic48 { get; set; }

        [Required]
        public double CurrentAHarmonic49 { get; set; }

        [Required]
        public double CurrentAHarmonic50 { get; set; }

        [Required]
        public double CurrentBHarmonic0 { get; set; }

        [Required]
        public double CurrentBHarmonic1 { get; set; }

        [Required]
        public double CurrentBHarmonic2 { get; set; }

        [Required]
        public double CurrentBHarmonic3 { get; set; }

        [Required]
        public double CurrentBHarmonic4 { get; set; }

        [Required]
        public double CurrentBHarmonic5 { get; set; }

        [Required]
        public double CurrentBHarmonic6 { get; set; }

        [Required]
        public double CurrentBHarmonic7 { get; set; }

        [Required]
        public double CurrentBHarmonic8 { get; set; }

        [Required]
        public double CurrentBHarmonic9 { get; set; }

        [Required]
        public double CurrentBHarmonic10 { get; set; }

        [Required]
        public double CurrentBHarmonic11 { get; set; }

        [Required]
        public double CurrentBHarmonic12 { get; set; }

        [Required]
        public double CurrentBHarmonic13 { get; set; }

        [Required]
        public double CurrentBHarmonic14 { get; set; }

        [Required]
        public double CurrentBHarmonic15 { get; set; }

        [Required]
        public double CurrentBHarmonic16 { get; set; }

        [Required]
        public double CurrentBHarmonic17 { get; set; }

        [Required]
        public double CurrentBHarmonic18 { get; set; }

        [Required]
        public double CurrentBHarmonic19 { get; set; }

        [Required]
        public double CurrentBHarmonic20 { get; set; }

        [Required]
        public double CurrentBHarmonic21 { get; set; }

        [Required]
        public double CurrentBHarmonic22 { get; set; }

        [Required]
        public double CurrentBHarmonic23 { get; set; }

        [Required]
        public double CurrentBHarmonic24 { get; set; }

        [Required]
        public double CurrentBHarmonic25 { get; set; }

        [Required]
        public double CurrentBHarmonic26 { get; set; }

        [Required]
        public double CurrentBHarmonic27 { get; set; }

        [Required]
        public double CurrentBHarmonic28 { get; set; }

        [Required]
        public double CurrentBHarmonic29 { get; set; }

        [Required]
        public double CurrentBHarmonic30 { get; set; }

        [Required]
        public double CurrentBHarmonic31 { get; set; }

        [Required]
        public double CurrentBHarmonic32 { get; set; }

        [Required]
        public double CurrentBHarmonic33 { get; set; }

        [Required]
        public double CurrentBHarmonic34 { get; set; }

        [Required]
        public double CurrentBHarmonic35 { get; set; }

        [Required]
        public double CurrentBHarmonic36 { get; set; }

        [Required]
        public double CurrentBHarmonic37 { get; set; }

        [Required]
        public double CurrentBHarmonic38 { get; set; }

        [Required]
        public double CurrentBHarmonic39 { get; set; }

        [Required]
        public double CurrentBHarmonic40 { get; set; }

        [Required]
        public double CurrentBHarmonic41 { get; set; }

        [Required]
        public double CurrentBHarmonic42 { get; set; }

        [Required]
        public double CurrentBHarmonic43 { get; set; }

        [Required]
        public double CurrentBHarmonic44 { get; set; }

        [Required]
        public double CurrentBHarmonic45 { get; set; }

        [Required]
        public double CurrentBHarmonic46 { get; set; }

        [Required]
        public double CurrentBHarmonic47 { get; set; }

        [Required]
        public double CurrentBHarmonic48 { get; set; }

        [Required]
        public double CurrentBHarmonic49 { get; set; }

        [Required]
        public double CurrentBHarmonic50 { get; set; }

        [Required]
        public double CurrentCHarmonic0 { get; set; }

        [Required]
        public double CurrentCHarmonic1 { get; set; }

        [Required]
        public double CurrentCHarmonic2 { get; set; }

        [Required]
        public double CurrentCHarmonic3 { get; set; }

        [Required]
        public double CurrentCHarmonic4 { get; set; }

        [Required]
        public double CurrentCHarmonic5 { get; set; }

        [Required]
        public double CurrentCHarmonic6 { get; set; }

        [Required]
        public double CurrentCHarmonic7 { get; set; }

        [Required]
        public double CurrentCHarmonic8 { get; set; }

        [Required]
        public double CurrentCHarmonic9 { get; set; }

        [Required]
        public double CurrentCHarmonic10 { get; set; }

        [Required]
        public double CurrentCHarmonic11 { get; set; }

        [Required]
        public double CurrentCHarmonic12 { get; set; }

        [Required]
        public double CurrentCHarmonic13 { get; set; }

        [Required]
        public double CurrentCHarmonic14 { get; set; }

        [Required]
        public double CurrentCHarmonic15 { get; set; }

        [Required]
        public double CurrentCHarmonic16 { get; set; }

        [Required]
        public double CurrentCHarmonic17 { get; set; }

        [Required]
        public double CurrentCHarmonic18 { get; set; }

        [Required]
        public double CurrentCHarmonic19 { get; set; }

        [Required]
        public double CurrentCHarmonic20 { get; set; }

        [Required]
        public double CurrentCHarmonic21 { get; set; }

        [Required]
        public double CurrentCHarmonic22 { get; set; }

        [Required]
        public double CurrentCHarmonic23 { get; set; }

        [Required]
        public double CurrentCHarmonic24 { get; set; }

        [Required]
        public double CurrentCHarmonic25 { get; set; }

        [Required]
        public double CurrentCHarmonic26 { get; set; }

        [Required]
        public double CurrentCHarmonic27 { get; set; }

        [Required]
        public double CurrentCHarmonic28 { get; set; }

        [Required]
        public double CurrentCHarmonic29 { get; set; }

        [Required]
        public double CurrentCHarmonic30 { get; set; }

        [Required]
        public double CurrentCHarmonic31 { get; set; }

        [Required]
        public double CurrentCHarmonic32 { get; set; }

        [Required]
        public double CurrentCHarmonic33 { get; set; }

        [Required]
        public double CurrentCHarmonic34 { get; set; }

        [Required]
        public double CurrentCHarmonic35 { get; set; }

        [Required]
        public double CurrentCHarmonic36 { get; set; }

        [Required]
        public double CurrentCHarmonic37 { get; set; }

        [Required]
        public double CurrentCHarmonic38 { get; set; }

        [Required]
        public double CurrentCHarmonic39 { get; set; }

        [Required]
        public double CurrentCHarmonic40 { get; set; }

        [Required]
        public double CurrentCHarmonic41 { get; set; }

        [Required]
        public double CurrentCHarmonic42 { get; set; }

        [Required]
        public double CurrentCHarmonic43 { get; set; }

        [Required]
        public double CurrentCHarmonic44 { get; set; }

        [Required]
        public double CurrentCHarmonic45 { get; set; }

        [Required]
        public double CurrentCHarmonic46 { get; set; }

        [Required]
        public double CurrentCHarmonic47 { get; set; }

        [Required]
        public double CurrentCHarmonic48 { get; set; }

        [Required]
        public double CurrentCHarmonic49 { get; set; }

        [Required]
        public double CurrentCHarmonic50 { get; set; }
    }
}
