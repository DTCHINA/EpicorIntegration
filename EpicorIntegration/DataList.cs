using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
using Epicor.Mfg.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TableAdapterHelper;

namespace Epicor_Integration
{
    public class DataList
    {
        public static BLConnectionPool EpicConn = new BLConnectionPool(Properties.Settings.Default.uname, Properties.Settings.Default.passw, "AppServerDC://" + Properties.Settings.Default.svrname + ":" + Properties.Settings.Default.svrport);

        public static void EpicClose()
        {
                SessionMod SM = new SessionMod(EpicConn);
                
                SM.GracefulShutdown();

                Progress.Open4GL.DynamicAPI.Session Session = EpicConn.Get();

                EpicConn.Release(Session);
        }

        public static BOReader BOReader
        {
            get
            {
                BOReader BOReader = new BOReader(EpicConn);

                return BOReader;
            }

        }

        public static DataSet PlannerList()
        {
            DataSet ds = (DataSet)BOReader.GetList("Person", "", "PersonID,Name");

            EpicClose();

            return ds;
        }

        public static DataSet PlantDataSet()
        {
            DataSet ds = (DataSet)BOReader.GetList("Plant", "", "Company,Plant,Name");

            EpicClose();

            return ds;
        }

        public static DataSet PartClassDataSet()
        {
            DataSet ds = (DataSet)BOReader.GetList("PartClass", "", "ClassID,Description");

            EpicClose();

            return ds;
        }

        public static bool PartExists(string partnumber)
        {
            Part Part = new Part(EpicConn);

            PartDataSet Pdata = new PartDataSet();

            bool retval;

            try
            {
                retval = Part.PartExists(partnumber);

                //Pdata = Part.GetByID(partnumber);
            }
            catch
            {
                EpicClose();

                return false;
            }

            EpicClose();

            return retval;
        }

        public static PartDataSet GetPart(string partnumber)
        {
            Part Part = new Part(EpicConn);

            PartDataSet Pdata = new PartDataSet();

            try
            {
                Pdata = Part.GetByID(partnumber);
            }
            catch { }

            EpicClose();

            return Pdata;
        }

        public static string AdvanceRevision(string CurrentRevision)
        {
            char[] InVal = CurrentRevision.ToUpper().ToCharArray();

            long retval = 0;

            for (int i = InVal.GetUpperBound(0); i >= 0; i--)
            {
                int charval = (int)InVal[i];

                int x = InVal.GetUpperBound(0);

                retval += (int)Math.Pow(26, x - i) * ((int)InVal[i] - 64);

            }

            retval++;

            string s = "";
            for (long i = (long)Convert.ToDouble(Math.Log(Convert.ToDouble(25 * (Convert.ToDouble(retval) + 1))) / Math.Log(26)) - 1; i >= 0; i--)
            {
                long x = (long)Convert.ToDouble(Math.Pow(26, i + 1) - 1) / 25 - 1;
                if (retval > x)
                {
                    s += (char)(((retval - x - 1) / Convert.ToDouble(Math.Pow(26, i))) % 26 + 65);
                }
            }
            return s;
        }

        public static DataSet EngWB_DS(string GroupID, string PartNumber, string Rev)
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            EngWorkBenchDataSet EngWBDS = new EngWorkBenchDataSet();

            EngWBDS = EngWB.GetDatasetForTree(GroupID,PartNumber,Rev, "", DateTime.Today, false, false);

            return (DataSet)EngWBDS;
        }

        /// <summary>
        /// Adds data in specified column at row number and table all into PartDataSet given
        /// </summary>
        /// <param name="Part"></param>
        /// <param name="tableName"></param>
        /// <param name="rowNum"></param>
        /// <param name="colName"></param>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static PartDataSet AddDatum(PartDataSet Part, string tableName, int rowNum, string colName, string Input, DataViewRowState RowState)
        {
            DataTable PartDT = Part.Tables[tableName];

            DataRow[] WorkRow = PartDT.Select(null, null, RowState);

            WorkRow[0] = PartDT.Rows[rowNum];

            try
            {
                WorkRow[0][colName] = Input;
            }
            catch (System.Exception ex)
            {
                try
                {
                    WorkRow[0][colName] = double.Parse(Input);
                }
                catch //(System.Exception ex1)
                {
                    try
                    {
                        WorkRow[0][colName] = (int)(double.Parse(Input));
                    }
                    catch //(System.Exception ex2)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                }

            }

            return Part;
        }

        public static PartDataSet UpdateDatum(PartDataSet Part, string tableName, int rowNum, string colName, string Input)
        {
            try
            {
                Part.Tables[tableName].Rows[rowNum][colName] = Input;

                return Part;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return Part;	
            }
        }

        public static DataSet ProdGrupDataSet()
        {
            DataSet ds = (DataSet)BOReader.GetList("ProdGrup", "", "ProdCode,Description");

            EpicClose();

            return ds;
        }

        public static DataSet UOMSearchDataSet()
        {
            DataSet ds = (DataSet)BOReader.GetList("UOMSearch", "((Active=True) AND (UOMClassID = 'NORCO'))", "UOMCode,UOMDesc");

            EpicClose();

            return ds;
        }

        public static DataSet UOMClassDataSet()
        {
            BOReader BOReader = new BOReader(EpicConn);

            DataSet ds = (DataSet)BOReader.GetList("UOMClass", "((Active=True) AND (ClassType<>'OnTheFly'))", "UOMClassID,Description");

            EpicClose();

            return ds;
        }

        public static DataSet UOMWeightDataSet()
        {
            DataSet ds = (DataSet)BOReader.GetList("UOMSearch", "((Active=True) AND (ClassType='Weight'))", "UOMCode,UOMDesc");

            EpicClose();

            return ds;
        }

        public static DataSet UOMVolumeDataSet()
        {
            DataSet ds = (DataSet)BOReader.GetList("UOMSearch", "((Active=True) AND (ClassType='Volume'))", "UOMCode,UOMDesc");

            EpicClose();

            return ds;
        }

        public static DataSet WarehseDataSet()
        {
            //Should diversify this call to use other than MfgSys as plant
            DataSet ds = (DataSet)BOReader.GetList("WarehseSearch", "MfgSys", "");

            EpicClose();

            return ds;
        }

        public static DataSet GroupIDDataSet()
        {
            bool MorePages;

            EngWorkBench EngBench = new EngWorkBench(EpicConn);

            DataSet ds = (DataSet)EngBench.GetList(" BY GroupID", 100, 0, out MorePages);

            EpicClose();

            return ds;
        }

        public static DataSet ResourceGroup()
        {
            DataSet ds = (DataSet)BOReader.GetList("ResourceGroup", "Inactive=False", "ResourceGrpID,Inactive,Description");

            EpicClose();

            return ds;
        }

        public static DataSet Resource(string ResourceGrpId)
        {
            DataSet ds = (DataSet)BOReader.GetList("Resource", "(Inactive=False) AND ResourceGrpID='" + ResourceGrpId +"'", "ResourceID,Inactive,ResourceGrpId,Description");

            EpicClose();

            return ds;
        }

        /// <summary>
        /// Checks to see if part is checked out by someone.  If it is returns true and with a message regarding who and when.
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="PartNumber"></param>
        /// <param name="Revision"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool PartCheckOutStatus(string GroupID, string PartNumber,string Revision, out string Message)
        {
            Message = "";

            bool retval = false;

            try
            {
                EngWorkBench EngWb = new EngWorkBench(EpicConn);

                EngWorkBenchDataSet EngWbDS = new EngWorkBenchDataSet();

                EngWbDS = EngWb.GetDatasetForTree(GroupID, PartNumber, Revision, "", DateTime.Today, true, false);

                if (EngWbDS.Tables["ECORev"].Rows[0]["CheckedOut"].ToString() == "True" && EngWbDS.Tables["ECORev"].Rows[0]["CheckedOutBy"].ToString() != Environment.UserName.ToString())
                {
                    retval = true;

                    Message = "Item was checked out by " + EngWbDS.Tables["ECORev"].Rows[0]["CheckedOutBy"].ToString() + " on " + EngWbDS.Tables["ECORev"].Rows[0]["CheckOutDate"].ToString();
                }

                if (EngWbDS.Tables["ECORev"].Rows[0]["CheckedOut"].ToString() == "True" && EngWbDS.Tables["ECORev"].Rows[0]["CheckedOutBy"].ToString() == Environment.UserName.ToString())
                {
                    retval = true;

                    Message = "Checked Out by GroupID";
                }
            }
            catch { }

            return retval;
        }

        public static void CheckOutPart(string GroupID, string PartNumber, string Revision)
        {
            string CheckedOutRevNum;

            string altMethodMsg;

            bool altMethodFlg;

            EngWorkBench EngWb = new EngWorkBench(EpicConn);

            EngWb.CheckOut(GroupID, PartNumber, Revision, "", DateTime.Today, false, false, false, true, false, out CheckedOutRevNum, out altMethodMsg, out altMethodFlg);

            EpicClose();
        }

        public static void UndoCheckOutPart(string GroupID, string PartNumber, string Revision)
        {
            EngWorkBench EngWb = new EngWorkBench(EpicConn);

            EngWorkBenchDataSet ds = new EngWorkBenchDataSet();

            ds = EngWb.GetDatasetForTree(GroupID,PartNumber,Revision,"",DateTime.Today,false,true);

            EngWb.UndoCheckOut(GroupID, PartNumber, Revision,"", DateTime.Today, false, false, false, true, ds);
        }

        public static void ApprovePart(string GroupID,string PartNumber, string Revision)
        {
            EngWorkBench EngWb = new EngWorkBench(EpicConn);

            EngWorkBenchDataSet EngDataSet = EngWb.GetDatasetForTree(GroupID, PartNumber, Revision, "", DateTime.Now, false, false);

            EngDataSet.Tables["ECORev"].Rows[0]["Approved"] = true;

            EngWb.CheckECORevApproved(true, false, EngDataSet);

            EngWb.Update(EngDataSet);

            EpicClose();
        }

        public static void UnApproveOldRevisions(string GroupID, string PartNumber, string Revision)
        {
            try
            {
                Part Part = new Part(EpicConn);

                PartDataSet Pdata = Part.GetByID(PartNumber);

                for (int i = 0; i < Pdata.Tables["PartRev"].Rows.Count; i++)
                {
                    if (Pdata.Tables["PartRev"].Rows[i]["RevisionNum"].ToString() != Revision)
                    {
                        Pdata.Tables["PartRev"].Rows[i]["Approved"] = false;
                    }
                }

                Part.Update(Pdata);

                EpicClose();
            }
            catch { }
        }
        
        public static void CheckInPart(string GroupID, string PartNumber, string Revision)
        {
            EngWorkBench EngWb = new EngWorkBench(EpicConn);

            string opMessage;

            EngWb.CheckIn(GroupID, PartNumber, Revision, "", DateTime.Now, false, false, true, true, false, "FOR EPICOR INTEGRATION MODULE", out opMessage);

            EpicClose();
        }

        /// <summary>
        /// Search Function for retrieving Part lists
        /// </summary>
        /// <param name="WhereStatement">Equivalent to the SQL WHERE function; Leave blank for all possiblities</param>
        /// <returns>Dataset of parts meeting the WhereStatement criteria</returns>
        public PartDataSet PartSearchDataSet(string WhereStatement)
        {
            Part Part = new Part(EpicConn);

            bool More;

            DataSet dss = ((DataSet)Part.GetList(WhereStatement, 0, 0, out More));

            PartDataSet ds = (PartDataSet)dss;

            EpicClose();

            return ds;
        }

        public DataSet GetProjectRoles()
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            DataSet MethodReturned = EngWB.GetProjectRoles();

            EpicClose();

            return MethodReturned;
        }

        public string GetCodeDescList()
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            string ReturnMethod = EngWB.GetCodeDescList("ECORev", "ProcessMode");

            EpicClose();

            return ReturnMethod;
        }

        public void GetAvailTaskSets(out string NewList)
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            EngWB.GetAvailTaskSets(out NewList);

            EpicClose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipGroupID">ECO Group ID</param>
        public void GroupLock(string ipGroupID)
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            EngWB.GroupLock(ipGroupID);

            EpicClose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipGroupID">ECO Group ID</param>
        /// <param name="ipCheckOutStatus">Normally should be true</param>
        /// <returns></returns>
        public EngWorkBenchDataSet GetECORevData(string ipGroupID, bool ipCheckOutStatus)
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            EngWorkBenchDataSet ReturnMethod = EngWB.GetECORevData(ipGroupID, ipCheckOutStatus);

            EpicClose();

            return ReturnMethod;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipGroupID">ECO Group ID</param>
        /// <param name="ipPartNum">Part Number</param>
        /// <param name="ipRevisionNum">Revision Number</param>
        /// <param name="ipAltMethod">Normally Null string</param>
        /// <param name="ipAsOfDate">Current Date/Time</param>
        /// <param name="ipCompleteTree">Normally False</param>
        /// <param name="ipUseMEthodForParts">Normally True</param>
        /// <returns></returns>
        public EngWorkBenchDataSet GetDatasetForTree(string ipGroupID, string ipPartNum, string ipRevisionNum, string ipAltMethod, DateTime? ipAsOfDate, bool ipCompleteTree, bool ipUseMEthodForParts)
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            EngWorkBenchDataSet MethodReturned = EngWB.GetDatasetForTree(ipGroupID,ipPartNum,ipRevisionNum,ipAltMethod,ipAsOfDate,ipCompleteTree,ipUseMEthodForParts);

            EpicClose();

            return MethodReturned;
        }

        public ECOGroupListDataSet GetList(string ipGroupID)
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            bool morePages;

            ECOGroupListDataSet ReturnMethod = EngWB.GetList("GroupID = '" + ipGroupID + "' BY GroupID", 0, 0, out morePages);

            EpicClose();

            return ReturnMethod;
        }

        public EngWorkBenchDataSet CheckOut(string ipGroupID, string ipPartNum, string ipRevisionNum, string ipAltMethod, DateTime? ipAsOfDate, bool ipCompleteTree, bool ipValidPassword, bool ipReturn, bool ipGetDatasetForTree, bool ipUseMethodForParts, out string opCheckedOutRevisionNum, out string altMethodMsg, out bool altMethodFlg)
        {
            EngWorkBench EngWB = new EngWorkBench(EpicConn);

            EngWorkBenchDataSet ReturnMethod = EngWB.CheckOut(ipGroupID, ipPartNum, ipRevisionNum, ipAltMethod, ipAsOfDate, ipCompleteTree, ipValidPassword, ipReturn, ipGetDatasetForTree, ipUseMethodForParts, out opCheckedOutRevisionNum, out altMethodMsg, out altMethodFlg);

            EpicClose();

            return ReturnMethod;
        }

        public static string GetCurrentDesc(string PartNumber)
        {
            try
            {
                Part Part = new Part(EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                int LastRowIndex = PartData.Tables["Part"].Rows.Count - 1;

                string PartDesc = PartData.Tables["Part"].Rows[LastRowIndex]["PartDescription"].ToString();

                EpicClose();

                return PartDesc;
            }
            catch { return null; }
        }

        public static string GetCurrentRev(string PartNumber)
        {
            try
            {
                Part Part = new Part(DataList.EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                int LastRowIndex = PartData.Tables["PartRev"].Rows.Count - 1;

                string PartRev = PartData.Tables["PartRev"].Rows[LastRowIndex]["RevisionNum"].ToString();

                EpicClose();

                return PartRev;
            }
            catch { return ""; }
        }

        public static bool GetSerialized(string PartNumber)
        {
            try
            {
                Part Part = new Part(EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                int LastRowIndex = PartData.Tables["Part"].Rows.Count - 1;

                bool PartDesc = bool.Parse(PartData.Tables["Part"].Rows[LastRowIndex]["TrackSerialNum"].ToString());

                EpicClose();

                return PartDesc;
            }
            catch { return false; }
        }

        public static string GetType(string PartNumber)
        {
            try
            {
                Part Part = new Part(DataList.EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                int LastRowIndex = PartData.Tables["Part"].Rows.Count - 1;

                string PartInfo = PartData.Tables["Part"].Rows[LastRowIndex]["TypeCode"].ToString();

                if (PartInfo == "M")
                    PartInfo = "Manufactured";

                if (PartInfo == "P")
                    PartInfo = "Purchased";

                if (PartInfo == "K")
                    PartInfo = "Sales Kit";

                EpicClose();

                return PartInfo;
            }
            catch { return ""; }
        }

        public static string GetGroup(string PartNumber)
        {
            try
            {
                Part Part = new Part(DataList.EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                int LastRowIndex = PartData.Tables["Part"].Rows.Count - 1;

                string PartInfo = PartData.Tables["Part"].Rows[LastRowIndex]["ProdCode"].ToString();

                DataTable DT = DataList.ProdGrupDataSet().Tables[0];

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (DT.Rows[i]["ProdCode"].ToString() == PartInfo)
                    {
                        PartInfo = DT.Rows[i]["Description"].ToString();

                        break;
                    }
                }

                EpicClose();

                return PartInfo;
            }
            catch { return ""; }
        }

        public static string GetClass(string PartNumber)
        {
            try
            {
                Part Part = new Part(DataList.EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                int LastRowIndex = PartData.Tables["Part"].Rows.Count - 1;

                string PartInfo = PartData.Tables["Part"].Rows[LastRowIndex]["ClassID"].ToString();

                DataTable DT = DataList.PartClassDataSet().Tables[0];

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (DT.Rows[i]["ClassID"].ToString() == PartInfo)
                    {
                        PartInfo = DT.Rows[i]["Description"].ToString();

                        break;
                    }
                }

                EpicClose();

                return PartInfo;
            }
            catch { return ""; }
        }
        
        public static DataTable PartUOM(string PartNumber)
        {
            try
            {
                Part Part = new Part(EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                EpicClose();

                return PartData.Tables["PartUOM"];
            }
            catch { return null; }
        }

        public static EngWorkBenchDataSet GetDetailsFromMethods(string GroupID, string ToPnum, string ToPnumRev, string FromPnum, string FromPnumRev)
        {
            EngWorkBenchDataSet EngWBDS = new EngWorkBenchDataSet();

            try
            {
                EngWorkBench EngWB = new EngWorkBench(EpicConn);

                string vMessage;

                EngWB.PreGetDetails(GroupID, ToPnum, ToPnumRev, "", FromPnum, FromPnumRev, "", out vMessage);

                EngWBDS = EngWB.GetDetailsFromMethods(GroupID, ToPnum, ToPnumRev, "", DateTime.Today.Date, false, FromPnum, FromPnumRev, "", true, true, false, false, false);

                return EngWBDS;
            }
            catch (Exception Exception)
            {
                System.Windows.Forms.MessageBox.Show(Exception.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return EngWBDS;
        }

        public static bool CreatePartRevision(string PartNumber, string CurrentRev, string NewRev, string RevDesc, string RevComment, string ECOnum)
        {
            bool _results;

            try
            {
                Part Part = new Part(DataList.EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                Part.GetNewPartRev(PartData, PartNumber, CurrentRev);

                int Y = PartData.Tables["PartRev"].Rows.Count - 1;

                DataList.UpdateDatum(PartData, "PartRev", Y, "RevShortDesc", RevDesc);

                DataList.UpdateDatum(PartData, "PartRev", Y, "RevDescription", RevComment);

                DataList.UpdateDatum(PartData, "PartRev", Y, "RevisionNum", NewRev);

                DataList.UpdateDatum(PartData, "PartRev", Y, "AltMethod", "");

                DataList.UpdateDatum(PartData, "PartRev", Y, "ECO", ECOnum);

                Part.Update(PartData);

                _results = true;
            }
            catch { _results = false; }
            finally
            {
                EpicClose();
            }

            return _results;
        }

        #region Safe Lists

        public static List<RawMaterial> GetCoils()
        {
            Part Part = new Part(DataList.EpicConn);

            PartListDataSet PartList = new PartListDataSet();

            string WhereClause = "ClassID = 'PC05'";

            bool morePages;

            PartList = Part.GetList(WhereClause, 0, 0, out morePages);

            DataList.EpicClose();

            List<RawMaterial> CoilNumbers = new List<RawMaterial>();

            foreach (DataRow DR in PartList.Tables[0].Rows)
            {
                RawMaterial val = new RawMaterial();

                val.part_number = DR["PartNum"].ToString();

                val.description = DR["PartDescription"].ToString();

                CoilNumbers.Add(val);
            }

            return CoilNumbers;
        }

        public static List<RawMaterial> GetEcoat()
        {
            Part Part = new Part(DataList.EpicConn);

            PartListDataSet PartList = new PartListDataSet();

            string WhereClause = "ClassID = 'PCNC'";

            bool morePages;

            PartList = Part.GetList(WhereClause, 0, 0, out morePages);

            DataList.EpicClose();

            List<RawMaterial> EcoatNumber = new List<RawMaterial>();

            foreach (DataRow DR in PartList.Tables[0].Rows)
            {
                RawMaterial val = new RawMaterial();

                val.part_number = DR["PartNum"].ToString();

                val.description = DR["PartDescription"].ToString();

                EcoatNumber.Add(val);
            }

            return EcoatNumber;
        }

        public static List<RawMaterial> GetSheets()
        {
            Part Part = new Part(DataList.EpicConn);

            PartListDataSet PartList = new PartListDataSet();

            string WhereClause = "ClassID = 'PC04'";

            bool morePages;

            PartList = Part.GetList(WhereClause, 0, 0, out morePages);

            DataList.EpicClose();

            List<RawMaterial> SheetNumbers = new List<RawMaterial>();

            foreach (DataRow DR in PartList.Tables[0].Rows)
            {
                RawMaterial val = new RawMaterial();

                val.part_number = DR["PartNum"].ToString();

                val.description = DR["PartDescription"].ToString();

                SheetNumbers.Add(val);
            }

            return SheetNumbers;
        }

        public static DateTime? EffectiveDate(string PartNumber)
        {
            DateTime? retval = new DateTime?();

            try
            {
                Part Part = new Part(EpicConn);

                PartDataSet PartData = new PartDataSet();

                PartData = Part.GetByID(PartNumber);

                int LastRowIndex = PartData.Tables["Part"].Rows.Count - 1;

                string PartDesc = PartData.Tables["Part"].Rows[LastRowIndex]["EffectiveDate"].ToString();

                EpicClose();
            }
            catch { retval = null; }

            return retval;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    class PartTypeCode
    {
        private string _Code;
        private string _Description;

        public PartTypeCode(string Description, string Code)
        {
            _Code = Code;
            _Description = Description;
        }

        /// <summary>
        /// Overridden to prove correct data member for combobox
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _Code;
        }

        public string Code
        {
            get
            {
                return _Code;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
        }
    }

    public class ProdStdType
    {
        private string _Code;
        private string _Desc;

        public ProdStdType(string Description, string Code)
        {
            _Code = Code;
            _Desc = Description;
        }

        public override string ToString()
        {
            return _Code;
        }

        public string Code
        {
            get
            {
                return _Code;
            }
        }

        public string Description
        {
            get
            {
                return _Desc;
            }
        }
    }

    public class MtlReseqType
    {
        private string _Name;
        private string _Arg;

        public MtlReseqType(string Name, string Arg)
        {
            _Name = Name;

            _Arg = Arg;
        }

        public override string ToString()
        {
            return _Arg;
        }

        public string Arg
        {
            get
            {
                return _Arg;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
        }
    }

     /// <summary>
    /// Data structure for Part with all appropriate descriptors
    /// </summary>
    public class PartData
    {
        public string PartNumber;

        public string Description;

        public string PMT;

        public string UOM_Class;

        public decimal Net_Weight;

        public decimal Net_Vol;

        public string Net_Weight_UM;

        public string Net_Vol_UM;

        public string Primary_UOM;

        public string PartGroup;

        public string PartClass;

        public string PartPlant;

        public bool Phantom;

        private List<string> _PlantWhse = new List<string>();

        private List<string> _PlantWhse_Code = new List<string>();

        public List<string> PlantWhse
        {
            get { return _PlantWhse; }
            set { _PlantWhse = value; }
        }

        public List<string> PlantWhse_Code
        {
            get { return _PlantWhse_Code; }
            set { _PlantWhse_Code = value; }
        }

        public bool QtyBearing;

        public bool UseRevision;

        public bool TrackSerial;

        public string TrackSerial_Mask;

        public string Planner;
    }

    public class RawMaterial : IComparable<RawMaterial>
    {
        public string part_number { get; set; }
        public string description { get; set; }

        public int CompareTo(RawMaterial other)
        {
            if (this.description == other.description)
            {
                return this.part_number.CompareTo(other.part_number);
            }

            return other.description.CompareTo(this.description);
        }

        public override string ToString()
        {
            return this.part_number.ToString() + " - " + this.description.ToString();
        }
    }

    public static class Templates
    {
        public static DataTable GetBomTemplates()
        {
            ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable RetVal = new ENGDataDataSet.TemplatesDataTable();

            TableAdapter.FillByType(RetVal, "BOM");

            return (DataTable)RetVal;
        }

        public static DataTable GetBomTemplates(SqlTransaction Trans)
        {
            ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable RetVal = new ENGDataDataSet.TemplatesDataTable();

            TableAdapter.FillByType(RetVal, "BOM");

            return (DataTable)RetVal;
        }

        public static DataTable GetOomTemplates()
        {
            ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable RetVal = new ENGDataDataSet.TemplatesDataTable();

            TableAdapter.FillByType(RetVal, "OOM");

            return (DataTable)RetVal;
        }

        public static DataTable GetResTemplates()
        {
            ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable RetVal = new ENGDataDataSet.TemplatesDataTable();

            TableAdapter.FillByType(RetVal, "RES");

            return (DataTable)RetVal;
        }

        public static DataTable GetItemTemplates()
        {
            ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable RetVal = new ENGDataDataSet.TemplatesDataTable();

            TableAdapter.FillByType(RetVal, "ITEM");

            return (DataTable)RetVal;
        }

        public static DataTable GetFullTemplate(string Name, string Type)
        {
            ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable RetVal = new ENGDataDataSet.TemplatesDataTable();

            TableAdapter.FillByNameType(RetVal, Type, Name);

            return (DataTable)RetVal;
        }

        public static DataTable GetFullTemplate(SqlTransaction Trans, string Name, string Type)
        {
            ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable RetVal = new ENGDataDataSet.TemplatesDataTable();
            /*
            if (Trans == null)
                TableHelper.BeginTransaction(TableAdapter);
            else
                TableHelper.SetTransaction(TableAdapter, Trans);*/

            TableAdapter.FillByNameType(RetVal, Type, Name);

            return (DataTable)RetVal;
        }

        public static PartData ParseItemTemplate(string TemplateName)
        {
            PartData Pdata = new PartData();

            ENGDataDataSetTableAdapters.TemplatesTableAdapter TableAdapter = new ENGDataDataSetTableAdapters.TemplatesTableAdapter();

            ENGDataDataSet.TemplatesDataTable DT = new ENGDataDataSet.TemplatesDataTable();

            TableAdapter.FillByNameType(DT, "ITEM", TemplateName);

            foreach (DataRow Dr in DT.Rows)
            {
                if (Dr["PropertyType"].ToString() == "TYPE")
                    Pdata.PMT = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "UOM")
                    Pdata.Primary_UOM = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "GROUP")
                    Pdata.PartGroup = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "QTYBEARING")
                    Pdata.QtyBearing = bool.Parse(Dr["PropertyValue"].ToString());

                if (Dr["PropertyType"].ToString() == "CLASS")
                    Pdata.PartClass = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "USEREV")
                    Pdata.UseRevision = bool.Parse(Dr["PropertyValue"].ToString());

                if (Dr["PropertyType"].ToString() == "TRACKSERIAL")
                {
                    Pdata.TrackSerial = bool.Parse(Dr["PropertyValue"].ToString());

                    Pdata.TrackSerial_Mask = Dr["PropertyOptions"].ToString();
                }

                if (Dr["PropertyType"].ToString() == "PLANT")
                    Pdata.PartPlant = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "WAREHOUSE")
                {
                    Pdata.PlantWhse.Add(Dr["PropertyValue"].ToString());

                    Pdata.PlantWhse_Code.Add(Dr["PropertyQty"].ToString());
                }

                if (Dr["PropertyType"].ToString() == "PLANNER")
                    Pdata.Planner = Dr["PropertyValue"].ToString();

                if (Dr["PropertyType"].ToString() == "WEIGHT_UOM")
                    Pdata.Net_Weight_UM = Dr["PropertyValue"].ToString();
            }

            return Pdata;
        }
    }
}
