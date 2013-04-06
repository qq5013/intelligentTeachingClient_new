using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Carbinet
{
    public partial class frmLegend : MetroForm
    {
        int first_tile_top = 67;
        int first_label_top = 73;
        int tile_top_step = 33;
        public frmLegend(List<string> _textList, List<MetroColorStyle> _styleList)
            : this()
        {
            if (_textList != null && _textList.Count > 0)
            {
                for (int i = 0; i < _textList.Count; i++)
                {
                    string text = _textList[i];
                    MetroColorStyle style = _styleList[i];

                    MetroTile metroTile1 = new MetroFramework.Controls.MetroTile();
                    Controls.Add(metroTile1);
                    metroTile1.ActiveControl = null;
                    metroTile1.Location = new System.Drawing.Point(15, first_tile_top + i * tile_top_step);
                    metroTile1.MainText = "";
                    metroTile1.Name = "metroTile" + i.ToString();
                    metroTile1.Size = new System.Drawing.Size(85, 23);
                    metroTile1.Style = style;
                    metroTile1.StyleManager = null;
                    metroTile1.TabIndex = 0;
                    metroTile1.Text = "";
                    metroTile1.Theme = MetroFramework.MetroThemeStyle.Light;
                    metroTile1.TileCount = 0;

                    System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
                    this.Controls.Add(label1);
                    label1.AutoSize = true;
                    label1.ForeColor = System.Drawing.Color.White;
                    label1.Location = new System.Drawing.Point(108, first_label_top + i * tile_top_step);
                    label1.Name = "label" + i.ToString();
                    label1.Size = new System.Drawing.Size(95, 12);
                    label1.TabIndex = 3;
                    label1.Text = text;

                }
            }

        }
        public frmLegend()
        {
            InitializeComponent();
        }
    }
}
