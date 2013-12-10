using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdmLib;

namespace ECO_Helper
{
    public static class SWHelper
    {
        public static string GetCurrentRevision(string path, string partnumber)
        {
            object rev_val;

            IEdmVault7 vault = LogInVault();

            IEdmFolder5 ppoRetParentFolder;

            IEdmFile5 file = vault.GetFileFromPath(path, out ppoRetParentFolder);

            IEdmEnumeratorVariable5 var = file.GetEnumeratorVariable();

            string selected_config = DetermineConfig(file, vault, partnumber);

            var.GetVar("Revision", selected_config, out rev_val);

            return rev_val.ToString();
        }

        public static string GetCurrentDescription(string path, string partnumber)
        {
            object rev_val;

            IEdmVault7 vault = LogInVault();

            IEdmFolder5 ppoRetParentFolder;

            IEdmFile5 file = vault.GetFileFromPath(path, out ppoRetParentFolder);

            IEdmEnumeratorVariable5 var = file.GetEnumeratorVariable();

            string selected_config = DetermineConfig(file, vault, partnumber);

            var.GetVar("Description", selected_config, out rev_val);

            return rev_val.ToString();
        }

        public static decimal GetCurrentWeight(string path, string partnumber)
        {
            object rev_val;

            IEdmVault7 vault = LogInVault();

            IEdmFolder5 ppoRetParentFolder;

            IEdmFile5 file = vault.GetFileFromPath(path, out ppoRetParentFolder);

            IEdmEnumeratorVariable5 var = file.GetEnumeratorVariable();

            string selected_config = DetermineConfig(file, vault, partnumber);

            var.GetVar("NetWeight", selected_config, out rev_val);

            return decimal.Parse(rev_val.ToString());
        }

        public static IEdmVault7 LogInVault()
        {
            EdmVault5 vault = new EdmVault5();

            vault.LoginAuto("NORCO_PDM", 0);

            return vault;
        }

        public static string DetermineConfig(IEdmFile5 Part, IEdmVault7 vault, string partnumber)
        {
            string retval = "@";

            EdmStrLst5 list = Part.GetConfigurations();

            IEdmPos5 pos = list.GetHeadPosition();

            Config_Select config = new Config_Select(vault, Part, partnumber);

            pos = list.GetHeadPosition();

            for (int i = 0; i < list.Count; i++)
            {
                string itemtoadd = list.GetNext(pos);

                if (itemtoadd != "@")
                    config.config_cbo.Items.Add(itemtoadd);
            }

            config.config_cbo.SelectedIndex = 0;

            config.ShowDialog();

            retval = config.SelectedConfig;

            if (config.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return "";

            return retval;
        }
    }
}
