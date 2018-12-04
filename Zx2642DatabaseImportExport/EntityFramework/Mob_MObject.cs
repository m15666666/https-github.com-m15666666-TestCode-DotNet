using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zx2642DatabaseImportExport.EntityFramework
{
    /// <summary>
    /// Mob_MObject entity
    /// </summary>
    public class Mob_MObject
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Mobject_ID { get; set; }

        [NotMapped] // ignore 不映射成数据库字段
        public int Parent_ID { get; set; }

        public int Org_ID { get; set; }

        public string Mobject_CD { get; set; }

        public string MobjectName_TX { get; set; }

        public int? Spec_ID { get; set; }

        public int? ControlLevel_ID { get; set; }

        public string GGXH_TX { get; set; }

        public string PitureNo_TX { get; set; }

        public string CZ_TX { get; set; }

        public int? RepairType_ID { get; set; }

        public int Property_ID { get; set; }

        public int? DJOwner_ID { get; set; }

        public int? JXDept_ID { get; set; }

        public int? CZDept_ID { get; set; }

        public int? GNWZ_ID { get; set; }

        public string AZWZ_TX { get; set; }

        public string XLH_TX { get; set; }

        public string SBMS_TX { get; set; }

        public string SBJC_TX { get; set; }

        public string EnglishName_TX { get; set; }

        public int? ZJL_NR { get; set; }

        public string ZJLDW_TX { get; set; }

        public int? Status_ID { get; set; }

        public string ZZCJ_TX { get; set; }

        public int? ZCZT_ID { get; set; }

        public DateTime? CCRQ_DT { get; set; }

        public DateTime? AZRQ_DT { get; set; }

        public DateTime? TYRQ_DT { get; set; }

        public string AddUser_TX { get; set; }

        public DateTime? Add_DT { get; set; }

        public string EditUser_TX { get; set; }

        public DateTime? Edit_DT { get; set; }

        public string Active_YN { get; set; }

        public int? SortNo_NR { get; set; }

        public int? Area_ID { get; set; }

        public int? CostCenter_ID { get; set; }

        public string JSCS_TX { get; set; }

        public decimal? MobjectMoney_NR { get; set; }

        public string ZYBJ_TX { get; set; }

        public string SBWT_TX { get; set; }

        public string SBGZ { get; set; }

        public string JSCSFileName_TX { get; set; }

        public byte[] JSCSFile_BL { get; set; }

        public decimal? YXRL_NR { get; set; }

        public int? SBFL_ID { get; set; }

        public decimal? Cubage_NR { get; set; }

        public int? Class_ID { get; set; }

        public string ZOEE_TX { get; set; }

        public string ParentMobject_CD { get; set; }

        public string InterFaceKey_CD { get; set; }

        public int? Mobject_Level { get; set; }

        public int? SubOrg_ID { get; set; }

        public int? FaultType_ID { get; set; }

        public DateTime? LastChangeStatus_DT { get; set; }

        public string MobjectFullName_TX { get; set; }

        public int? KBJ_NR { get; set; }

        public int? YBJ_NR { get; set; }

        public decimal? Partition_ID { get; set; }

        public int? MobjectJX_ID { get; set; }

        public string RegCode_TX { get; set; }

        public DateTime? Reg_DT { get; set; }

        public string Designer_TX { get; set; }

        public string InstallationCom_TX { get; set; }

        public DateTime? LastCheck_DT { get; set; }

        public DateTime? NextCheck_DT { get; set; }

        public string LastCheckResult_TX { get; set; }

        public string LastCheckType_TX { get; set; }

        public int? DaysNotice_NR { get; set; }

        public int? BJ_ID { get; set; }

        public string XHPCode_CD { get; set; }

        public int? MPClass_ID { get; set; }

        public int? MPManageClass_ID { get; set; }

        public int? MPCalibrationCycle_NR { get; set; }

        public decimal? MPRangeLower_NR { get; set; }

        public decimal? MPRangeUpper_NR { get; set; }

        public decimal? MPError_NR { get; set; }

        public string MPCLFW_TX { get; set; }

        public string MPBQDD_TX { get; set; }

        public DateTime? LastBuild_DT { get; set; }

        public string JXDW_TX { get; set; }

        public string ConfirmUser_TX { get; set; }

        public string ConfirmResult_TX { get; set; }

        public DateTime? Confirm_DT { get; set; }

        public decimal? A_MPRangeLower_NR { get; set; }

        public decimal? A_MPRangeUpper_NR { get; set; }

        public decimal? A_MPError_NR { get; set; }

        public string A_CLFW_TX { get; set; }

        public string A_BQDD_TX { get; set; }

        public int? ZYX_ID { get; set; }
    }
}

