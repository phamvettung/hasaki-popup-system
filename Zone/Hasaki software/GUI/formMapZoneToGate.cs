using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Intech_software.GUI
{
    public partial class formMapZoneToGate : Form
    {
        private frmInBound _frmInBound = null;

        public formMapZoneToGate()
        {
            InitializeComponent();

            SetDefaultUI();
        }

        public formMapZoneToGate(Form form)
        {
            _frmInBound = form as frmInBound;
            InitializeComponent();

            SetDefaultUI();
        }

        private void SetDefaultUI()
        {
            List<string> gateSelection = new List<string>
            {
               "Cổng R1",
                "Cổng R2",
                "Cổng R3",
                "Cổng R4",
                "Cổng R5",
                "Cổng R6",
                "Cổng R7",
                "Cổng R8",
                "Cổng R9",
                "Cổng R10",
                "Cổng R11",
                "Cổng R12",
                "Cổng R13",
                "Cổng R14",
                "Cổng R15",
                "Cổng R16",
                "Cổng R17",
                "Cổng R18",
                "Cổng R19",
                "Cổng R20",
                "Cổng L1",
                "Cổng L2",
                "Cổng L3",
                "Cổng L4",
                "Cổng L5",
                "Cổng L6",
                "Cổng L7",
                "Cổng L8",
                "Cổng L9",
                "Cổng L10",
                "Cổng L11",
                "Cổng L12",
                "Cổng L13",
                "Cổng L14",
                "Cổng L15",
                "Cổng L16",
                "Cổng L17",
                "Cổng L18",
                "Cổng L19",
                "Cổng L20",
                "Cổng Lỗi"
            };


            cbSelectGr1.DataSource = gateSelection.ToArray();
            cbSelectGr2.DataSource = gateSelection.ToArray();
            cbSelectGr3.DataSource = gateSelection.ToArray();
            cbSelectGr4.DataSource = gateSelection.ToArray();
            cbSelectGr5.DataSource = gateSelection.ToArray();
            cbSelectGr6.DataSource = gateSelection.ToArray();
            cbSelectGr7.DataSource = gateSelection.ToArray();
            cbSelectGr8.DataSource = gateSelection.ToArray();

            ttGroup1.SetToolTip(cbSelectGroup1, "zone ha noi");

            if (_frmInBound != null)
            {
                var group1Selection = _frmInBound.GetGroupSelection(1);
                var group2Selection = _frmInBound.GetGroupSelection(2);
                var group3Selection = _frmInBound.GetGroupSelection(3);
                var group4Selection = _frmInBound.GetGroupSelection(4);
                var group5Selection = _frmInBound.GetGroupSelection(5);
                var group6Selection = _frmInBound.GetGroupSelection(6);
                var group7Selection = _frmInBound.GetGroupSelection(7);
                var group8Selection = _frmInBound.GetGroupSelection(8);
                foreach (var item in group1Selection)
                {
                    foreach (TreeNode node in cbSelectGroup1.Nodes)
                    {
                        if (node.Text == item.Key)
                        {
                            if (item.Value.IsNullOrEmpty())
                            {
                                node.Checked = false;
                            }
                            else
                            {
                                node.Checked = true;
                                cbSelectGr1.SelectedIndex = cbSelectGr1.FindStringExact(item.Value) == -1 ? 0 : cbSelectGr1.FindStringExact(item.Value);
                            }
                        }
                    }
                }

                foreach (var item in group2Selection)
                {
                    foreach (TreeNode node in cbSelectGroup2.Nodes)
                    {
                        if (node.Text == item.Key)
                        {
                            if (item.Value.IsNullOrEmpty())
                            {
                                node.Checked = false;
                            }
                            else
                            {
                                node.Checked = true;
                                cbSelectGr2.SelectedIndex = cbSelectGr2.FindStringExact(item.Value) == -1 ? 0 : cbSelectGr2.FindStringExact(item.Value);
                            }
                        }
                    }
                }

                foreach (var item in group3Selection)
                {
                    foreach (TreeNode node in cbSelectGroup3.Nodes)
                    {
                        if (node.Text == item.Key)
                        {
                            if (item.Value.IsNullOrEmpty())
                            {
                                node.Checked = false;
                            }
                            else
                            {
                                node.Checked = true;
                                cbSelectGr3.SelectedIndex = cbSelectGr3.FindStringExact(item.Value) == -1 ? 0 : cbSelectGr3.FindStringExact(item.Value);
                            }
                        }
                    }
                }

                foreach (var item in group4Selection)
                {
                    foreach (TreeNode node in cbSelectGroup4.Nodes)
                    {
                        if (node.Text == item.Key)
                        {
                            if (item.Value.IsNullOrEmpty())
                            {
                                node.Checked = false;
                            }
                            else
                            {
                                node.Checked = true;
                                cbSelectGr4.SelectedIndex = cbSelectGr4.FindStringExact(item.Value) == -1 ? 0 : cbSelectGr4.FindStringExact(item.Value);
                            }
                        }
                    }
                }

                foreach (var item in group5Selection)
                {
                    foreach (TreeNode node in cbSelectGroup5.Nodes)
                    {
                        if (node.Text == item.Key)
                        {
                            if (item.Value.IsNullOrEmpty())
                            {
                                node.Checked = false;
                            }
                            else
                            {
                                node.Checked = true;
                                cbSelectGr5.SelectedIndex = cbSelectGr5.FindStringExact(item.Value) == -1 ? 0 : cbSelectGr5.FindStringExact(item.Value);
                            }
                        }
                    }
                }


                foreach (var item in group6Selection)
                {
                    foreach (TreeNode node in cbSelectGroup6.Nodes)
                    {
                        if (node.Text == item.Key)
                        {
                            if (item.Value.IsNullOrEmpty())
                            {
                                node.Checked = false;
                            }
                            else
                            {
                                node.Checked = true;
                                cbSelectGr6.SelectedIndex = cbSelectGr6.FindStringExact(item.Value) == -1 ? 0 : cbSelectGr6.FindStringExact(item.Value);
                            }
                        }
                    }
                }

                foreach (var item in group7Selection)
                {
                    foreach (TreeNode node in cbSelectGroup7.Nodes)
                    {
                        if (node.Text == item.Key)
                        {
                            if (item.Value.IsNullOrEmpty())
                            {
                                node.Checked = false;
                            }
                            else
                            {
                                node.Checked = true;
                                cbSelectGr7.SelectedIndex = cbSelectGr7.FindStringExact(item.Value) == -1 ? 0 : cbSelectGr7.FindStringExact(item.Value);
                            }
                        }
                    }
                }

                foreach (var item in group8Selection)
                {
                    foreach (TreeNode node in cbSelectGroup8.Nodes)
                    {
                        if (node.Text == item.Key)
                        {
                            if (item.Value.IsNullOrEmpty())
                            {
                                node.Checked = false;
                            }
                            else
                            {
                                node.Checked = true;
                                cbSelectGr8.SelectedIndex = cbSelectGr8.FindStringExact(item.Value) == -1 ? 0 : cbSelectGr8.FindStringExact(item.Value);
                            }
                        }
                    }
                }

            }

        }
        private string getGateNumber(int gateIdx)
        {
            if (gateIdx < 1 || gateIdx > 40)
            {
                gateIdx = 1;
            }

            string gatePrefix = "R";

            if (gateIdx > 20)
            {
                gatePrefix = "L";
                gateIdx -= 20;

            }
            return gatePrefix + (gateIdx % 21).ToString().PadLeft(2);
        }

        private void panelMainZone_Click(object sender, System.EventArgs e)
        {

        }

        private void updasteCbSelectGroup1()
        {
            if (_frmInBound is null)
            {
                return;
            }

            Dictionary<string, string> mapZoneToGate = new Dictionary<string, string>();

            foreach (TreeNode item in cbSelectGroup1.Nodes)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if (mapZoneToGate.ContainsKey(item.Text))
                {
                    mapZoneToGate[item.Text] = cbSelectGr1.SelectedText;
                }
                else
                {
                    mapZoneToGate.Add(item.Text, cbSelectGr1.SelectedText);
                }
            }

            _frmInBound.SetDefaultSelectionGroup1(mapZoneToGate);
        }

        private void updasteCbSelectGroup2()
        {
            if (_frmInBound is null)
            {
                return;
            }

            Dictionary<string, string> mapZoneToGate = new Dictionary<string, string>();

            foreach (TreeNode item in cbSelectGroup2.Nodes)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if (mapZoneToGate.ContainsKey(item.Text))
                {
                    mapZoneToGate[item.Text] = cbSelectGr2.SelectedText;
                }
                else
                {
                    mapZoneToGate.Add(item.Text, cbSelectGr2.SelectedText);
                }
            }

            _frmInBound.SetDefaultSelectionGroup2(mapZoneToGate);
        }

        private void updasteCbSelectGroup3()
        {
            if (_frmInBound is null)
            {
                return;
            }

            Dictionary<string, string> mapZoneToGate = new Dictionary<string, string>();

            foreach (TreeNode item in cbSelectGroup3.Nodes)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if (mapZoneToGate.ContainsKey(item.Text))
                {
                    mapZoneToGate[item.Text] = cbSelectGr3.SelectedText;
                }
                else
                {
                    mapZoneToGate.Add(item.Text, cbSelectGr3.SelectedText);
                }
            }

            _frmInBound.SetDefaultSelectionGroup3(mapZoneToGate);
        }

        private void updasteCbSelectGroup4()
        {
            if (_frmInBound is null)
            {
                return;
            }

            Dictionary<string, string> mapZoneToGate = new Dictionary<string, string>();

            foreach (TreeNode item in cbSelectGroup4.Nodes)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if (mapZoneToGate.ContainsKey(item.Text))
                {
                    mapZoneToGate[item.Text] = cbSelectGr4.SelectedText;
                }
                else
                {
                    mapZoneToGate.Add(item.Text, cbSelectGr4.SelectedText);
                }
            }

            _frmInBound.SetDefaultSelectionGroup4(mapZoneToGate);
        }

        private void updasteCbSelectGroup5()
        {
            if (_frmInBound is null)
            {
                return;
            }

            Dictionary<string, string> mapZoneToGate = new Dictionary<string, string>();

            foreach (TreeNode item in cbSelectGroup5.Nodes)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if (mapZoneToGate.ContainsKey(item.Text))
                {
                    mapZoneToGate[item.Text] = cbSelectGr5.SelectedText;
                }
                else
                {
                    mapZoneToGate.Add(item.Text, cbSelectGr5.SelectedText);
                }
            }

            _frmInBound.SetDefaultSelectionGroup5(mapZoneToGate);
        }

        private void updasteCbSelectGroup6()
        {
            if (_frmInBound is null)
            {
                return;
            }

            Dictionary<string, string> mapZoneToGate = new Dictionary<string, string>();

            foreach (TreeNode item in cbSelectGroup6.Nodes)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if (mapZoneToGate.ContainsKey(item.Text))
                {
                    mapZoneToGate[item.Text] = cbSelectGr6.SelectedText;
                }
                else
                {
                    mapZoneToGate.Add(item.Text, cbSelectGr6.SelectedText);
                }
            }

            _frmInBound.SetDefaultSelectionGroup6(mapZoneToGate);
        }

        private void updasteCbSelectGroup7()
        {
            if (_frmInBound is null)
            {
                return;
            }

            Dictionary<string, string> mapZoneToGate = new Dictionary<string, string>();

            foreach (TreeNode item in cbSelectGroup7.Nodes)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if (mapZoneToGate.ContainsKey(item.Text))
                {
                    mapZoneToGate[item.Text] = cbSelectGr7.SelectedText;
                }
                else
                {
                    mapZoneToGate.Add(item.Text, cbSelectGr7.SelectedText);
                }
            }

            _frmInBound.SetDefaultSelectionGroup7(mapZoneToGate);
        }

        private void updasteCbSelectGroup8()
        {
            if (_frmInBound is null)
            {
                return;
            }

            Dictionary<string, string> mapZoneToGate = new Dictionary<string, string>();

            foreach (TreeNode item in cbSelectGroup8.Nodes)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if (mapZoneToGate.ContainsKey(item.Text))
                {
                    mapZoneToGate[item.Text] = cbSelectGr8.SelectedText;
                }
                else
                {
                    mapZoneToGate.Add(item.Text, cbSelectGr8.SelectedText);
                }
            }

            _frmInBound.SetDefaultSelectionGroup8(mapZoneToGate);
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_frmInBound is null)
            {
                return;
            }

            updasteCbSelectGroup1();
            updasteCbSelectGroup2();
            updasteCbSelectGroup3();
            updasteCbSelectGroup4();
            updasteCbSelectGroup5();
            updasteCbSelectGroup6();
            updasteCbSelectGroup7();
            updasteCbSelectGroup8();


            _frmInBound.SaveSelection();

            Close();
        }
    }
}
