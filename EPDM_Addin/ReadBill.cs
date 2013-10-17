using System;
using System.Collections.Generic;
using System.Diagnostics;
using SolidWorks.Interop.sldworks;

namespace SWAddin
{
    public class BillItem : IComparable<BillItem>
    {
        public string Parent_item { get; set; }

        public string Item { get; set; }

        public double Length { get; set; }

        public double Qty { get; set; }

        public string Description { get; set; }

        public int CompareTo(BillItem other)
        {
            return Item.CompareTo(other.Item);
        }
    }

    internal class ReadBill
    {
        public List<BillItem> ListToImport { get; set; }

        public BillItem AddRecord { get; set; }

        public List<List<BillItem>> BomList { get; set; }

        public SolidWorks.Interop.sldworks.SldWorks swApp { get; set; }

        public List<BillItem> ProcessTableAnn(SldWorks swApp, ModelDoc2 swModel, TableAnnotation swTableAnn, string ConfigName)
        {
            int nNumRow = 0;
            int J = 0;
            int I = 0;
            string ItemNumber = null;
            string PartNumber = null;

            nNumRow = swTableAnn.RowCount;

            BomTableAnnotation swBOMTableAnn = default(BomTableAnnotation);
            swBOMTableAnn = (BomTableAnnotation)swTableAnn;

            ListToImport = new List<BillItem>();

            for (J = 0; J <= nNumRow - 1; J++)
            {
                AddRecord = new BillItem();

                swBOMTableAnn.GetComponentsCount2(J, ConfigName, out ItemNumber, out PartNumber);

                if (ItemNumber != null && ItemNumber != "")
                {
                    int Qi = swBOMTableAnn.GetComponentsCount(J);
                    int Q = swBOMTableAnn.GetComponentsCount2(J, ConfigName, out ItemNumber, out PartNumber);
                    if (Q == 0)
                        Q = swBOMTableAnn.GetComponentsCount(J);
                    AddRecord.Qty = (double)Q;
                    AddRecord.Item = PartNumber;
                    AddRecord.Parent_item = swTableAnn.Title;
                }

                object[] vPtArr = null;
                Component2 swComp = null;

                vPtArr = (object[])swBOMTableAnn.GetComponents2(J, ConfigName);

                if (vPtArr == null)
                    vPtArr = (object[])swBOMTableAnn.GetComponents(J);

                if (vPtArr != null)
                {
                    for (I = 0; I <= vPtArr.GetUpperBound(0); I++)
                    {
                        swComp = (Component2)vPtArr[I];
                        if (swComp != null)
                        {
                            string Desc = null;

                            try
                            {
                                ModelDoc2 swModDoc2 = (ModelDoc2)swComp.GetModelDoc2();
                                ConfigurationManager swConfigMan = swModDoc2.ConfigurationManager;
                                CustomPropertyManager swCustPropMan = swConfigMan.ActiveConfiguration.CustomPropertyManager;
                                string ValOut;
                                string EvalValOut;
                                swCustPropMan.Get2("L", out ValOut, out EvalValOut);

                                Desc = swModDoc2.get_CustomInfo2(swComp.ReferencedConfiguration, "Description1");
                                if (Desc == null || Desc == "")
                                {
                                    Desc = swModDoc2.get_CustomInfo("Description1");
                                }

                                string PipeStatus = swModDoc2.get_CustomInfo("PIPE");
                                if (PipeStatus == "TRUE" || PartNumber.Contains("-R"))
                                {
                                    PartNumber = swModDoc2.get_CustomInfo2(swComp.ReferencedConfiguration, "Partnumber");
                                    if (PartNumber == null || PartNumber == "" || PartNumber.Contains("-R"))
                                    {
                                        PartNumber = swModDoc2.get_CustomInfo("Partnumber");
                                    }
                                }

                                AddRecord.Item = PartNumber;

                                AddRecord.Description = Desc;

                                EvalValOut = swModDoc2.get_CustomInfo2(swComp.ReferencedConfiguration, "L");

                                if (EvalValOut != null && EvalValOut != "" && EvalValOut != " ")
                                {
                                    try
                                    {
                                        AddRecord.Length = double.Parse(EvalValOut);
                                    }
                                    catch
                                    {
                                        System.Windows.Forms.DialogResult diag = System.Windows.Forms.MessageBox.Show("Item: " + AddRecord.Item + " has had a problem converting its length.  Rebuild and try again", "Error: Continue?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk, System.Windows.Forms.MessageBoxDefaultButton.Button1);

                                        if (diag == System.Windows.Forms.DialogResult.No)
                                            throw;

                                        AddRecord.Item = null;
                                    }
                                }
                            }
                            catch
                            {
                                System.Windows.Forms.MessageBox.Show("Error on Part Number: " + PartNumber + "\nQuantity and Lengths may be wrong for this item.", "Warning!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            Debug.Print("  Could not get component.");
                        }
                    }
                }

                if (AddRecord.Item != null && AddRecord.Qty != 0 && AddRecord.Item.Trim() != "")
                {
                    ListToImport.Add(AddRecord);
                }
            }

            return ListToImport;
        }

        public void ProcessBomFeature(SldWorks swApp, ModelDoc2 swModel, BomFeature swBomFeat)
        {
            Feature swFeat = default(Feature);
            object[] vTableArr = null;
            object vTable = null;
            string[] vConfigArray = null;
            object vConfig = null;
            string ConfigName = null;
            TableAnnotation swTable = default(TableAnnotation);
            object visibility = null;

            swFeat = swBomFeat.GetFeature();

            vTableArr = (object[])swBomFeat.GetTableAnnotations();

            foreach (TableAnnotation vTable_loopVariable in vTableArr)
            {
                vTable = vTable_loopVariable;
                swTable = (TableAnnotation)vTable;
                vConfigArray = (string[])swBomFeat.GetConfigurations(true, ref visibility);
                foreach (object vConfig_loopVariable in vConfigArray)
                {
                    vConfig = vConfig_loopVariable;
                    ConfigName = (string)vConfig;
                    List<BillItem> Temp = new List<BillItem>();
                    Temp = ProcessTableAnn(swApp, swModel, swTable, ConfigName);
                    try
                    {
                        Temp.Sort();

                        string[,] TempBill = new string[4, UniqueItemCount(Temp)];

                        TempBill = CompileLength(Temp);

                        int i = TempBill.GetUpperBound(1);

                        Temp.Clear();

                        for (int j = 0; j < i + 1; j++)
                        {
                            BillItem New = new BillItem();

                            New.Item = TempBill[0, j];

                            New.Length = double.Parse(TempBill[2, j]);

                            int TempQty = (int)(double.Parse(TempBill[1, j]) * 100);

                            New.Qty = (double)(TempQty) / 100;

                            New.Parent_item = TempBill[3, j];

                            New.Description = TempBill[4, j];

                            Temp.Add(New);
                        }

                        Temp.Sort();

                        BomList.Add(Temp);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("Error: Bill List was empty", "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                        throw new IndexOutOfRangeException();
                    }
                }
            }

            //Launch Dialog with BomList as param
        }

        public string[,] CompileLength(List<BillItem> Condensing)
        {
            string[,] lbom = new string[5, Condensing.Count];

            for (int i = 0; i < Condensing.Count; i++)
            {
                lbom[0, i] = Condensing[i].Item;

                lbom[1, i] = Condensing[i].Qty.ToString();

                lbom[2, i] = Condensing[i].Length.ToString();

                lbom[3, i] = Condensing[i].Parent_item;

                lbom[4, i] = Condensing[i].Description;
            }

            string[,] bom = new string[5, UniqueItemCount(Condensing)];

            string[] temp = new string[UniqueItemCount(Condensing)];

            temp = UniqueItemArray(Condensing);

            #region Condense cut lengths

            for (int i = 0; i < temp.Length; i++)
            {
                double l = 0;

                for (int j = 0; j < lbom.GetUpperBound(1) + 1; j++)
                {
                    if (temp[i] == lbom[0, j])
                    {
                        double lbom2;

                        double.TryParse(lbom[2, j], out lbom2);

                        l = l + (double.Parse(lbom[1, j]) * lbom2);

                        bom[0, i] = temp[i];
                        bom[1, i] = lbom[1, j];
                        bom[2, i] = l.ToString();
                        if (bom[2, i] != "0")
                            bom[1, i] = bom[2, i];
                    }
                }
            }

            #endregion Condense cut lengths

            #region Compile Like Qty

            for (int i = 0; i < temp.Length; i++)
            {
                double countadd = 0;

                for (int j = 0; j < lbom.GetUpperBound(1) + 1; j++)
                {
                    if (temp[i] == lbom[0, j])
                    {
                        //bom[0, i] = temp[i];

                        countadd = countadd + double.Parse(lbom[1, j]);

                        //bom[1, i] = (double.Parse (lbom[1, j]) + double.Parse (bom[1,i])).ToString ();
                    }
                }

                bom[0, i] = temp[i];

                bom[1, i] = countadd.ToString();
            }

            #endregion Compile Like Qty

            for (int i = 0; i < temp.Length; i++)
            {
                bom[2, i] = Math.Round((double.Parse(bom[2, i]) / 12), 3, System.MidpointRounding.AwayFromZero).ToString();

                if (bom[2, i] != "0")
                    bom[1, i] = bom[2, i];
            }

            for (int i = 0; i < bom.GetUpperBound(1) + 1; i++)
            {
                for (int j = 0; j < Condensing.Count; j++)
                {
                    if (bom[0, i] == Condensing[j].Item)
                    {
                        bom[3, i] = Condensing[j].Parent_item;

                        bom[4, i] = Condensing[j].Description;
                    }
                }
            }

            return bom;
        }

        public List<BillItem> CompileQty(List<BillItem> Condensing)
        {
            string[] Temp = new string[UniqueItemCount(Condensing)];

            Temp = UniqueItemArray(Condensing);

            string[,] FalseArray = new string[2, UniqueItemCount(Condensing)];

            List<BillItem> TempBill = new List<BillItem>();

            BillItem TempAdd = new BillItem();

            for (int i = 0; i < Temp.Length; i++)
            {
                double countadd = 0;

                for (int j = 0; j < Condensing.Count; j++)
                {
                    if (Temp[i] == Condensing[j].Item)
                    {
                        countadd = countadd + Condensing[j].Qty;
                    }
                }

                FalseArray[0, i] = Temp[i];

                FalseArray[1, i] = countadd.ToString();
            }

            for (int i = 0; i < FalseArray.GetUpperBound(1) + 1; i++)
            {
                BillItem TempAdd2 = new BillItem();

                TempAdd2.Item = FalseArray[0, i];

                TempAdd2.Qty = double.Parse(FalseArray[1, i]);

                //TempAdd2.Parent_item =

                TempBill.Add(TempAdd2);
            }

            return TempBill;
        }

        private string[] UniqueItemArray(List<BillItem> Array)
        {
            string[] result = new string[UniqueItemCount(Array)];

            int count = 0;

            string element = Array[0].Item;

            result[count++] = element;

            for (int i = 1; i < Array.Count; i++)
            {
                if (element == Array[i].Item)
                    continue;
                else
                {
                    element = Array[i].Item;
                    result[count++] = element;
                }
            }
            return result;
        }

        private int UniqueItemCount(List<BillItem> Array)
        {
            string element = Array[0].Item;

            int count = 1;

            for (int i = 0; i < Array.Count; i++)
            {
                if (element == Array[i].Item)
                    continue;
                else
                {
                    element = Array[i].Item;
                    count++;
                }
            }

            return count;
        }

        private ISldWorks iSwApp { get; set; }

        public void Main(SwAddin addin)
        {
            SwAddin userAddin = addin;
            iSwApp = (ISldWorks)userAddin.SwApp;
            swApp = (SldWorks)iSwApp;
            ModelDoc2 swModel = default(ModelDoc2);
            DrawingDoc swDraw = default(DrawingDoc);
            Feature swFeat = default(Feature);
            BomFeature swBomFeat = default(BomFeature);
            BomList = new List<List<BillItem>>();

            swModel = (ModelDoc2)swApp.ActiveDoc;
            swDraw = (DrawingDoc)swModel;
            swFeat = (Feature)swModel.FirstFeature();

            while ((swFeat != null))
            {
                if ("BomFeat" == swFeat.GetTypeName())
                {
                    swBomFeat = (BomFeature)swFeat.GetSpecificFeature2();

                    ProcessBomFeature(swApp, swModel, swBomFeat);
                }
                swFeat = (Feature)swFeat.GetNextFeature();
            }

            for (int i = 0; i < BomList.Count; i++)
            {
                PrintResult(BomList[i]);

                BillImport form = new BillImport(BomList[i]);

                form.ShowDialog();

                if (form.DialogResult != System.Windows.Forms.DialogResult.OK)
                {
                    form.Dispose();

                    System.Windows.Forms.MessageBox.Show("Process Cancelled by User!", "WARNING!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);

                    break;
                }

                form.Dispose();
            }
        }

        public void PrintResult(List<BillItem> Result)
        {
            Debug.Print("////////////////////////////////////");

            for (int i = 0; i < Result.Count; i++)
            {
                Debug.Print("***");

                Debug.Print("Parent Part: " + Result[i].Parent_item);

                Debug.Print("Part number: " + Result[i].Item);

                Debug.Print("Description: " + Result[i].Description);

                Debug.Print("Length: " + Result[i].Length);

                Debug.Print("Qty: " + Result[i].Qty);

                Debug.Print("***");
            }

            Debug.Print("/////////////////////////////////////");
        }
    }

    internal class ReadCutList
    {
        private ISldWorks iSwApp { get; set; }

        public SolidWorks.Interop.sldworks.SldWorks swApp { get; set; }

        private string Parent_item { get; set; }

        public void Main(SwAddin addin)
        {
            SwAddin userAddin = addin;
            iSwApp = (ISldWorks)userAddin.SwApp;
            swApp = (SldWorks)iSwApp;

            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;
            Feature swFeat = (Feature)swModel.FirstFeature();
            ListToImport = new List<BillItem>();

            Parent_item = swModel.GetTitle();

            Parent_item = Parent_item.Substring(0, Parent_item.IndexOf("."));

            TraverseFeat(swFeat, true);

            ListToImport.Sort();

            CompileQty(ListToImport);

            PrintResult(ListToImport);

            BillImport form = new BillImport(ListToImport);

            form.ShowDialog();

            form.Dispose();
        }

        public void PrintResult(List<BillItem> Result)
        {
            Debug.Print("////////////////////////////////////");

            for (int i = 0; i < Result.Count; i++)
            {
                Debug.Print("Parent Part: " + Result[i].Parent_item);

                Debug.Print("Part number: " + Result[i].Item);

                Debug.Print("Length: " + Result[i].Length);

                Debug.Print("Qty: " + Result[i].Qty);
            }

            Debug.Print("/////////////////////////////////////");
        }

        public bool FindWeldment(Feature ThisFeat, bool IsTop)
        {
            bool ret_val = false;
            object Bodies = new Body2[100];
            Feature CurFeat = ThisFeat;
            while (!(CurFeat == null))
            {
                string FeatType = CurFeat.GetTypeName();

                if (FeatType == "WeldmentFeature")
                    ret_val = true;

                Feature NextFeat = null;
                if (IsTop)
                    NextFeat = (Feature)CurFeat.GetNextFeature();

                CurFeat = NextFeat;
                NextFeat = null;
            }

            return ret_val;
        }

        public void TraverseFeat(Feature ThisFeat, bool IsTop)
        {
            object Bodies = new Body2[100];
            Feature CurFeat = ThisFeat;
            while (!(CurFeat == null))
            {
                DoTheWork(CurFeat, Bodies);

                Feature SubFeat = (Feature)CurFeat.GetFirstSubFeature();
                SubFeat = null;

                Feature NextFeat = null;
                if (IsTop)
                    NextFeat = (Feature)CurFeat.GetNextFeature();
                CurFeat = NextFeat;
                NextFeat = null;
            }
        }

        private void DoTheWork(Feature ThisFeat, object Bodies)
        {
            AddRecord = new BillItem();
            bool IsBodyFolder = false;
            string FeatType = ThisFeat.GetTypeName();

            if (FeatType == "SolidBodyFolder" || FeatType == "CutListFolder" || FeatType == "SubWeldFolder" || FeatType == "SubAtomFolder" || FeatType == "SurfaceBodyFolder")
                IsBodyFolder = true;

            if (IsBodyFolder)
            {
                BodyFolder BFolder = (BodyFolder)ThisFeat.GetSpecificFeature2();

                Bodies = BFolder.GetBodies();

                if (!(Bodies == null))
                {
                    object[] Bod = (object[])Bodies;
                    int UpperBody = Bod.GetUpperBound(0);
                    for (int i = 0; i <= UpperBody; i++)
                    {
                        Body2 Body;
                        Body = (Body2)Bod[i];
                        string NameStr = Body.Name.ToString();
                        string NameTrim;
                        try
                        {
                            NameTrim = NameStr.Substring(0, 13).Trim();
                        }
                        catch
                        {
                            NameTrim = NameStr;
                        }

                        AddRecord.Item = ThisFeat.CustomPropertyManager.Get("WeldmentPartNumber");
                        try
                        {
                            string ValOut = "";
                            string ResolvedValOut = "";
                            string name = "LENGTH";

                            ThisFeat.CustomPropertyManager.Get2(name, out ValOut, out ResolvedValOut);

                            AddRecord.Qty = double.Parse(ResolvedValOut) / 12;

                            //for cutlist items - Qty == Length
                            //All have Qty 1 of X Length
                        }
                        catch
                        {
                            if (AddRecord.Qty == 0.0)
                                AddRecord.Qty = 1;
                        }

                        //AddRecord .Qty = (double)BFolder.GetBodyCount();

                        ListToImport.Add(AddRecord);
                    }
                }
            }
        }

        private string[] UniqueItemArray(List<BillItem> Array)
        {
            string[] result = new string[UniqueItemCount(Array)];

            int count = 0;

            string element = Array[0].Item;

            result[count++] = element;

            for (int i = 1; i < Array.Count; i++)
            {
                if (element == Array[i].Item)
                    continue;
                else
                {
                    element = Array[i].Item;
                    result[count++] = element;
                }
            }
            return result;
        }

        private int UniqueItemCount(List<BillItem> Array)
        {
            string element = Array[0].Item;

            int count = 1;

            for (int i = 0; i < Array.Count; i++)
            {
                if (element == Array[i].Item)
                    continue;
                else
                {
                    element = Array[i].Item;
                    count++;
                }
            }

            return count;
        }

        public void CompileQty(List<BillItem> Condensing)
        {
            string[] Temp = new string[UniqueItemCount(Condensing)];

            Temp = UniqueItemArray(Condensing);

            string[,] FalseArray = new string[2, UniqueItemCount(Condensing)];

            List<BillItem> TempBill = new List<BillItem>();

            BillItem TempAdd = new BillItem();

            for (int i = 0; i < Temp.Length; i++)
            {
                double countadd = 0;

                for (int j = 0; j < Condensing.Count; j++)
                {
                    if (Temp[i] == Condensing[j].Item)
                    {
                        countadd = countadd + Condensing[j].Qty;
                    }
                }

                FalseArray[0, i] = Temp[i];

                FalseArray[1, i] = countadd.ToString();
            }

            for (int i = 0; i < FalseArray.GetUpperBound(1) + 1; i++)
            {
                BillItem TempAdd2 = new BillItem();

                TempAdd2.Item = FalseArray[0, i];

                TempAdd2.Qty = (double)((int)((double.Parse(FalseArray[1, i])) * 100)) / 100;

                TempAdd2.Parent_item = Parent_item;

                TempBill.Add(TempAdd2);
            }

            ListToImport.Clear();

            ListToImport = TempBill;
        }

        public List<BillItem> ListToImport { get; set; }

        public BillItem AddRecord { get; set; }
    }
}