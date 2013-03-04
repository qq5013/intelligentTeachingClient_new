#region Copyright ?005, Cristi Potlog - All Rights Reserved
/* -------------------------------------------------------------- *
 *      Copyright ?005, Cristi Potlog - All Rights reserved      *
 *                                                                *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY *
 * OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT    *
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR    *
 * FITNESS FOR A PARTICULAR PURPOSE.                              *
 *                                                                *
 * THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.       *
 * -------------------------------------------------------------- */
#endregion Copyright ?005, Cristi Potlog - All Rights Reserved

#region References
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
#endregion

namespace CristiPotlog.ChartControl
{

    #region Enums
    /// <summary>
    /// ...
    /// </summary>
    public enum BackgroundImageStyle
    {
        CenterImage = 0,
        TileImage = 1,
        StretchImage = 2,
        UnscalledImage = 3
    }

    /// <summary>
    /// Specifies the types of tick marks for the chart control.
    /// </summary>
    public enum ChartTickMarkTypes
    {
        /// <summary>
        /// No tick marks are displayed.
        /// </summary>
        None,
        /// <summary>
        /// Tick marks are displayed inside the chart area.
        /// </summary>
        Inside,
        /// <summary>
        /// Tick marks are displayed outside the chart area.
        /// </summary>
        Outside,
        /// <summary>
        /// Tick marks are displayed both inside and outside the chart area.
        /// </summary>
        Cross
    }

    /// <summary>
    /// Specifies the styles of the grid lines for the chart control.
    /// </summary>
    public enum ChartGridStyles
    {
        /// <summary>
        /// No grid line is displayed.
        /// </summary>
        None,
        /// <summary>
        /// Minor grid lines are displayed.
        /// </summary>
        Minor,
        /// <summary>
        /// Major grid lines are displayed.
        /// </summary>
        Major
    }

    /// <summary>
    /// Specifies the time units for the time scale of the chart control.
    /// </summary>
    public enum ChartTimeUnits
    {
        /// <summary>
        /// The time scale is expressed in days.
        /// </summary>
        Days,
        /// <summary>
        /// The time scale is expressed in months.
        /// </summary>
        Months,
        /// <summary>
        /// The time scale is expressed in years.
        /// </summary>
        Years
    }

    /// <summary>
    /// Specifies the shapes of the value series marks for the chart control.
    /// </summary>
    public enum ChartMarkShapes
    {
        /// <summary>
        /// No marks are displayed.
        /// </summary>
        None,
        /// <summary>
        /// The marks are displayed as circles.
        /// </summary>
        Circle,
        /// <summary>
        /// The marks are displayed as diamonds.
        /// </summary>
        Diamond,
        /// <summary>
        /// The marks are displayed as squares.
        /// </summary>
        Square,
        /// <summary>
        /// The marks are displayed as triangles.
        /// </summary>
        Triangle
    }

    /// <summary>
    /// Specifies the position at which the legend will be displayed on the chart control.
    /// </summary>
    public enum ChartLegendPosition
    {
        /// <summary>
        /// The legend is displayed on top of the chart.
        /// </summary>
        Top,
        /// <summary>
        /// The legend is displayed on the left of the chart.
        /// </summary>
        Left,
        /// <summary>
        /// The legend is displayed on the right of the chart.
        /// </summary>
        Right,
        /// <summary>
        /// The legend is displayed bellow of the chart.
        /// </summary>
        Bottom
    }

    #endregion

    /// <summary>
    /// Represents a basic time series charting control.
    /// </summary>
    public class Chart : Control
    {
        #region Consts
        private const int MARK_SIZE = 3;
        private const int LARGE_TICK_SIZE = 6;
        private const int SMALL_TICK_SIZE = 3;
        private const int BORDER_PADDING_SIZE = 6;
        private const int TITLE_PADDING_SIZE = 6;
        private const int LEGEND_MARGIN = 4;
        private const float LEGEND_MAX_WIDTH = 0.25F; // max percents that legend can strech
        private static readonly Color defaultBackColor = SystemColors.Window;
        private static readonly Color defaultForeColor = SystemColors.WindowText;
        private const BackgroundImageStyle defaultBackgroundImageStyle = BackgroundImageStyle.CenterImage;
        #endregion

        #region Fields
        private BackgroundImageStyle backgroundImageStyle = Chart.defaultBackgroundImageStyle;
        private ChartTitleSettings title = null;
        private ChartTimeAxisSettings xAxis = null;
        private ChartValueAxisSettings yAxis = null;
        private ChartGridSettings grid = null;
        private ChartLegendSettings legend = null;
        private ChartSeriesSettings series = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of class Chart.
        /// </summary>
        public Chart()
        {
            // set control's name
            this.Name = this.GetType().Name;
            base.Text = this.Name;

            // set chart contents
            this.title = new ChartTitleSettings(this);
            this.xAxis = new ChartTimeAxisSettings(this);
            this.yAxis = new ChartValueAxisSettings(this);
            this.grid = new ChartGridSettings(this);
            this.legend = new ChartLegendSettings(this);
            this.series = new ChartSeriesSettings(this);

            // set control style to improove rendering (reduce flicker)
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            // initialize members
            base.BackColor = Chart.defaultBackColor;
            base.ForeColor = Chart.defaultForeColor;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets/sets the background image drawing style for the panel.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(Chart.defaultBackgroundImageStyle)]
        [Description("Gets/sets the background image drawing style of the control.")]
        public BackgroundImageStyle BackgroundImageStyle
        {
            get
            {
                return this.backgroundImageStyle;
            }
            set
            {
                if (this.backgroundImageStyle != value)
                {
                    this.backgroundImageStyle = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets/sets the background color of the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets/sets the background color of the control.")]
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (value == Color.Empty)
                {
                    base.BackColor = Chart.defaultBackColor;
                }
                else
                {
                    base.BackColor = value;
                }
            }
        }
        protected bool ShouldSerializeBackColor()
        {
            return (base.BackColor != Chart.defaultBackColor);
        }

        /// <summary>
        /// Gets/sets the foreground color of the control.  
        /// </summary>
        [Category("Appearance")]
        [Description("Gets/sets the foreground color of the control.")]
        public new Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                if (value == Color.Empty)
                {
                    base.ForeColor = Chart.defaultForeColor;
                }
                else
                {
                    base.ForeColor = value;
                }
            }
        }
        protected bool ShouldSerializeForeColor()
        {
            return (base.ForeColor != Chart.defaultForeColor);
        }

        /// <summary>
        /// Gets the text associated with this control.
        /// </summary>
        [Browsable(false)]
        [Description("Gets the text associated with this control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text
        {
            get
            {
                return base.Text;
            }
        }

        /// <summary>
        /// Determines the display settings for the title of the chart control.
        /// </summary>
        [Browsable(true)]
        [Category("Chart")]
        [Description("Determines the display settings for the title of the chart control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChartTitleSettings Title
        {
            get
            {
                return this.title;
            }
        }

        /// <summary>
        /// Determines the display settings for the time axis of the chart control.
        /// </summary>
        [Category("Chart")]
        [Description("Determines the display settings for the time axis of the chart control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChartTimeAxisSettings XAxis
        {
            get
            {
                return this.xAxis;
            }
        }

        /// <summary>
        /// Determines the display settings for the value axis of the chart control.
        /// </summary>
        [Category("Chart")]
        [Description("Determines the display settings for the value axis of the chart control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChartValueAxisSettings YAxis
        {
            get
            {
                return this.yAxis;
            }
        }

        /// <summary>
        /// Determines the display settings for the grid of the chart control.
        /// </summary>
        [Category("Chart")]
        [Description("Determines the display settings for the grid of the chart control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChartGridSettings Grid
        {
            get
            {
                return this.grid;
            }
        }

        /// <summary>
        /// Determines the display settings for the legend of the chart control.
        /// </summary>
        [Category("Chart")]
        [Description("Determines the display settings for the legend of the chart control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChartLegendSettings Legend
        {
            get
            {
                return this.legend;
            }
        }

        /// <summary>
        /// Determines the display settings for the series of the chart control.
        /// </summary>
        [Category("Chart")]
        [Description("Determines the display settings for the series of the chart control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChartSeriesSettings Series
        {
            get
            {
                return this.series;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            // call base class implementation
            base.OnPaint(pe);

            // get drawing rectangle
            Rectangle chartArea = this.ClientRectangle;

            // process backgound drawing
            CustomGraphics.DrawControlBackgroundImage(pe.Graphics, this, chartArea, this.BackgroundImage, this.backgroundImageStyle);

            // exclude border padding from drawind area
            chartArea.Inflate(-BORDER_PADDING_SIZE, -BORDER_PADDING_SIZE);

            // determine chart title's rectangle
            Rectangle titleRect = chartArea;
            if (this.title.Text.Length != 0)
            {
                // adjust rectangle to fit chart title
                int titleHeight = (int)pe.Graphics.MeasureString(this.title.Text, this.title.Font, 0, StringFormat.GenericTypographic).Height;
                titleHeight += TITLE_PADDING_SIZE;
                titleRect.Height = titleHeight;

                // exclude chart title from drawind area
                chartArea.Y += titleHeight;
                chartArea.Height -= titleHeight;
            }

            // draw chart title
            MeasureChartEventArgs me = new MeasureChartEventArgs(pe.Graphics, titleRect);
            this.OnMeasureChartTitle(me);
            this.OnDrawChartTitle(new DrawChartEventArgs(pe.Graphics, me));

            // check if chart legened is displayed
            if (this.legend.Visible)
            {
                // determine chart legend drawing area
                Size itemSize = this.GetLegendItemSize(pe.Graphics);

                // determine legend border size
                int borderWidth = (int)Math.Ceiling(this.legend.Border.Weight);
                int legendWidth = borderWidth > 0 ? borderWidth - 1 : 0;
                int legendHeight = legendWidth;

                legendWidth += Chart.LEGEND_MARGIN * 4;
                legendWidth += Chart.MARK_SIZE * 4;
                legendWidth += itemSize.Width;
                legendHeight += Chart.LEGEND_MARGIN * 2;
                legendHeight += itemSize.Height;

                // declare legend area rectangle
                Rectangle legendArea = chartArea;

                switch (this.legend.Position)
                {
                    case ChartLegendPosition.Top:
                        // determine the legend rectangle
                        legendArea.X = (legendArea.Right - legendWidth) / 2;

                        // exclude legend from drawind area
                        chartArea.Y += legendHeight;
                        chartArea.Height -= legendHeight;
                        break;
                    case ChartLegendPosition.Left:
                        // determine the legend rectangle
                        legendArea.Y = (legendArea.Bottom - legendHeight) / 2;

                        // exclude legend from drawind area
                        chartArea.X += legendWidth;
                        chartArea.Width -= legendWidth;
                        break;
                    case ChartLegendPosition.Right:
                        // determine the legend rectangle
                        legendArea.X = legendArea.Right - legendWidth;
                        legendArea.Y = (legendArea.Bottom - legendHeight) / 2;

                        // exclude legend from drawind area
                        chartArea.Width -= legendWidth;
                        break;
                    case ChartLegendPosition.Bottom:
                        // determine the legend rectangle
                        legendArea.X = (legendArea.Right - legendWidth) / 2;
                        legendArea.Y = legendArea.Bottom - legendHeight;

                        // exclude legend from drawind area
                        chartArea.Height -= legendHeight;
                        break;
                }

                // determine legend size
                legendArea.Size = new Size(legendWidth, legendHeight);

                // draw chart legend
                this.OnMeasureChartLegend(me = new MeasureChartEventArgs(pe.Graphics, legendArea));
                this.OnDrawChartLegend(new DrawChartEventArgs(pe.Graphics, me));
            }

            // determine X axis title's rectangle
            Rectangle xTitleRect = chartArea;
            if (this.xAxis.Title.Text.Length != 0)
            {
                // NOTE: X axis title affect the chart's height (Y axis size)
                // adjust rectangle to fit X axis title
                int xTitleHeight = (int)pe.Graphics.MeasureString(this.xAxis.Title.Text, this.xAxis.Title.Font, 0, StringFormat.GenericTypographic).Height;
                xTitleHeight += TITLE_PADDING_SIZE;
                xTitleRect.Y = chartArea.Bottom - xTitleHeight;
                xTitleRect.Height = xTitleHeight;

                // exclude X axis title from drawind area
                chartArea.Height -= xTitleHeight;
            }

            // determine X axis weight
            int xAxisWeight = (int)Math.Ceiling(this.xAxis.Line.Weight);

            // check if Y axis labels are visible
            if (this.yAxis.Labels.Visible)
            {
                // NOTE: Y axis labels affect the chart's width (X axis size)
                float maxWidth = pe.Graphics.MeasureString(this.yAxis.Scale.Maximum.ToString(), this.yAxis.Labels.Font, 0, StringFormat.GenericTypographic).Width;
                xAxisWeight += (int)maxWidth;
            }

            // draw X axis title
            this.OnMeasureChartXAxisTitle(me = new MeasureChartEventArgs(pe.Graphics, xTitleRect));
            this.OnDrawChartXAxisTitle(new DrawChartEventArgs(pe.Graphics, me));

            // determine Y axis title's rectangle
            Rectangle yTitleRect = chartArea;
            if (this.yAxis.Title.Text.Length != 0)
            {
                // NOTE: Y axis title affect the chart's width (X axis size)
                // adjust rectangle to fit Y axis title
                int yTitleHeight = (int)pe.Graphics.MeasureString(this.yAxis.Title.Text, this.yAxis.Title.Font, 0, StringFormat.GenericTypographic).Height;
                yTitleHeight += TITLE_PADDING_SIZE;
                yTitleRect.Width = yTitleHeight;

                // exclude Y axis title from drawind area
                chartArea.X += yTitleHeight;
                chartArea.Width -= yTitleHeight;
            }

            // determine Y axis weight
            int yAxisWeight = (int)Math.Ceiling(this.yAxis.Line.Weight);

            // check if X axis labels are visible
            if (this.xAxis.Labels.Visible)
            {
                // NOTE: X axis labels affect the chart's height (Y axis size)
                float maxHeight = pe.Graphics.MeasureString(this.xAxis.Scale.Maximum.ToString(), this.xAxis.Labels.Font, 0, StringFormat.GenericTypographic).Height;
                yAxisWeight += (int)maxHeight;
            }

            // draw Y axis title
            this.OnMeasureChartYAxisTitle(me = new MeasureChartEventArgs(pe.Graphics, yTitleRect));
            this.OnDrawChartYAxisTitle(new DrawChartEventArgs(pe.Graphics, me));

            // adjust chart drawing region
            chartArea.Inflate(-LARGE_TICK_SIZE, -LARGE_TICK_SIZE);
            chartArea.X += xAxisWeight;
            chartArea.Width -= xAxisWeight;
            chartArea.Height -= yAxisWeight;

            // draw chart X axis
            this.OnMeasureChartXAxis(me = new MeasureChartEventArgs(pe.Graphics, chartArea));
            this.OnDrawChartXAxis(new DrawChartEventArgs(pe.Graphics, me));

            // draw chart Y axis
            this.OnMeasureChartYAxis(me = new MeasureChartEventArgs(pe.Graphics, chartArea));
            this.OnDrawChartYAxis(new DrawChartEventArgs(pe.Graphics, me));

            // draw chart glid lines
            this.OnMeasureChartGrid(me = new MeasureChartEventArgs(pe.Graphics, chartArea));
            this.OnDrawChartGrid(new DrawChartEventArgs(pe.Graphics, me));

            // draw chart value lines
            this.OnMeasureChartLines(me = new MeasureChartEventArgs(pe.Graphics, chartArea));
            this.OnDrawChartLines(new DrawChartEventArgs(pe.Graphics, me));
        }

        /// <summary>
        /// Raises the MeasureChartTitle event.
        /// </summary>
        /// <param name="e">A MeasureChartEventArgs object that contains the event data.</param>
        protected virtual void OnMeasureChartTitle(MeasureChartEventArgs e)
        {
            // check if there are subscribers
            if (this.MeasureChartTitle != null)
            {
                // raise the MeasureChartTitle event.
                this.MeasureChartTitle(this, e);
            }
        }

        /// <summary>
        /// Raises the DrawChartTitle event.
        /// </summary>
        /// <param name="e">A DrawChartEventArgs object that contains the event data.</param>
        protected virtual void OnDrawChartTitle(DrawChartEventArgs e)
        {
            // check if there are subscribers
            if (this.DrawChartTitle != null)
            {
                // raise the DrawChartTitle event.
                this.DrawChartTitle(this, e);
            }

            // check if there is text to draw
            if (e.Cancel == false && this.title.Text.Length != 0)
            {
                // initialize graphic resources
                Brush textBrush = new SolidBrush(this.title.Color);
                try
                {
                    // draw the chart's title
                    StringFormat format = CustomGraphics.StringFormat(ContentAlignment.MiddleCenter);
                    e.Graphics.DrawString(this.title.Text, this.title.Font, textBrush, e.Bounds, format);
                }
                finally
                {
                    // free graphic resources
                    textBrush.Dispose();
                }
            }
        }

        /// <summary>
        /// Raises the MeasureChartLegend event.
        /// </summary>
        /// <param name="e">A MeasureChartEventArgs object that contains the event data.</param>
        protected virtual void OnMeasureChartLegend(MeasureChartEventArgs e)
        {
            // check if there are subscribers for the event
            if (this.MeasureChartLegend != null)
            {
                // raise the MeasureChartLegend event.
                this.MeasureChartLegend(this, e);
            }
        }

        /// <summary>
        /// Raises the DrawChartLegend event.
        /// </summary>
        /// <param name="e">A DrawChartEventArgs object that contains the event data.</param>
        protected virtual void OnDrawChartLegend(DrawChartEventArgs e)
        {
            // check if there are subscribers for the event
            if (this.DrawChartLegend != null)
            {
                // raise the DrawChartLegend event.
                this.DrawChartLegend(this, e);
            }

            // check if there is text to draw
            if (e.Cancel == false && this.legend.Visible)
            {
                // initialize graphic resources
                Pen borderPen = this.legend.Border.ToPen();
                Brush textBrush = new SolidBrush(this.series.Title.Color);
                Pen linePen = this.series.Line.ToPen();
                Pen markBorderPen = new Pen(this.series.Mark.BorderColor);
                Brush markFillPen = new SolidBrush(this.series.Mark.FillColor);
                try
                {
                    // draw the chart's legend
                    StringFormat format = CustomGraphics.StringFormat(ContentAlignment.MiddleLeft);
                    format.Trimming = StringTrimming.EllipsisCharacter;

                    // determine legend item size
                    Size itemSize = this.GetLegendItemSize(e.Graphics);

                    int borderWidth = (int)Math.Ceiling(this.legend.Border.Weight / 2);
                    borderWidth = borderWidth > 0 ? borderWidth - 1 : 0;

                    Point lineCap = e.Bounds.Location;
                    lineCap.Offset(borderWidth, borderWidth);
                    lineCap.Offset(Chart.LEGEND_MARGIN, Chart.LEGEND_MARGIN);
                    lineCap.Offset(0, Math.Max(itemSize.Height / 2, Chart.MARK_SIZE));

                    Point lineCap2 = lineCap;
                    lineCap2.Offset(Chart.MARK_SIZE * 4, 0);

                    // draw sample series line & mark
                    e.Graphics.DrawLine(linePen, lineCap, lineCap2);
                    lineCap.Offset(Chart.MARK_SIZE * 2, 0);
                    this.DoDrawChartMark(e.Graphics, markBorderPen, markFillPen, lineCap, this.series.Mark.Shape, Chart.MARK_SIZE);
                    lineCap2.Offset(Chart.LEGEND_MARGIN, 0);

                    // draw series title
                    Rectangle itemRect = new Rectangle(lineCap2, new Size(itemSize.Width + Chart.LEGEND_MARGIN * 2, 0));
                    e.Graphics.DrawString(this.series.Title.Text, this.series.Title.Font, textBrush, itemRect, format);

                    // check if we need to draw the lened's border
                    if (this.legend.Border.Visible && this.legend.Border.Weight > 0)
                    {
                        // draw legend's border
                        e.Graphics.DrawRectangle(borderPen, e.Bounds);
                    }
                }
                finally
                {
                    // free graphic resources
                    borderPen.Dispose();
                    textBrush.Dispose();
                    linePen.Dispose();
                    markBorderPen.Dispose();
                    markFillPen.Dispose();
                }
            }
        }

        /// <summary>
        /// Raises the MeasureChartXAxisTitle event.
        /// </summary>
        /// <param name="e">A MeasureChartEventArgs object that contains the event data.</param>
        protected virtual void OnMeasureChartXAxisTitle(MeasureChartEventArgs e)
        {
            // check if there are subscribers
            if (this.MeasureChartXAxisTitle != null)
            {
                // raise the MeasureChartXAxisTitle event.
                this.MeasureChartXAxisTitle(this, e);
            }
        }

        /// <summary>
        /// Raises the DrawChartXAxisTitle event.
        /// </summary>
        /// <param name="e">A DrawChartEventArgs object that contains the event data.</param>
        protected virtual void OnDrawChartXAxisTitle(DrawChartEventArgs e)
        {
            // check if there are subscribers
            if (this.DrawChartXAxisTitle != null)
            {
                // raise the DrawChartXAxisTitle event.
                this.DrawChartXAxisTitle(this, e);
            }

            // check if there is text to draw
            if (e.Cancel == false && this.xAxis.Title.Text.Length != 0)
            {
                // initialize graphic resources
                Brush textBrush = new SolidBrush(this.xAxis.Title.Color);
                try
                {
                    // draw the time axis title
                    StringFormat format = CustomGraphics.StringFormat(ContentAlignment.MiddleCenter);
                    e.Graphics.DrawString(this.xAxis.Title.Text, this.xAxis.Title.Font, textBrush, e.Bounds, format);
                }
                finally
                {
                    // free graphics resources
                    textBrush.Dispose();
                }
            }
        }

        /// <summary>
        /// Raises the MeasureChartXAxis event.
        /// </summary>
        /// <param name="e">A MeasureChartEventArgs object that contains the event data.</param>
        protected virtual void OnMeasureChartXAxis(MeasureChartEventArgs e)
        {
            // check if there are subscribers
            if (this.MeasureChartXAxis != null)
            {
                // raise the MeasureChartXAxis event.
                this.MeasureChartXAxis(this, e);
            }
        }

        /// <summary>
        /// Raises the DrawChartXAxis event.
        /// </summary>
        /// <param name="e">A DrawChartEventArgs object that contains the event data.</param>
        protected virtual void OnDrawChartXAxis(DrawChartEventArgs e)
        {
            // check if there are subscribers
            if (this.DrawChartXAxis != null)
            {
                // raise the DrawChartXAxis event.
                this.DrawChartXAxis(this, e);
            }

            // check if we need draw
            if (e.Cancel == false && this.xAxis.Line.Visible && this.xAxis.Line.Weight > 0)
            {
                // determine chart's origin points
                Point start = e.Bounds.Location;
                start.Offset(0, e.Bounds.Height);
                Point end = e.Bounds.Location;
                end.Offset(e.Bounds.Width, e.Bounds.Height);

                // initialize graphic resources
                Pen linePen = this.xAxis.Line.ToPen();
                Brush labelBrush = new SolidBrush(this.xAxis.Labels.Color);
                try
                {
                    // draw the axis line
                    e.Graphics.DrawLine(linePen, start, end);

                    // initialize variable for tick marks drawing
                    DateTime minDate = this.xAxis.Scale.Minimum;
                    DateTime maxDate = this.xAxis.Scale.Maximum;
                    TimeSpan dateDiff = (maxDate - minDate);
                    int min = 0;
                    int max = dateDiff.Days;
                    int length = end.X - start.X;
                    int step = this.xAxis.Scale.MajorUnit;

                    if (step > 0 && max > min)
                    {
                        // loop to draw the major tick marks (if needed)
                        //for (int tick = min + step; tick < max; tick += step)
                        for (int tick = min; tick <= max; tick += step)
                        {
                            // calculate tick's position on the axis
                            double proc = (double)(tick - min) / (max - min);
                            int tickOffset = (int)Math.Floor(proc * length);
                            DateTime tickDate = minDate.AddDays(tick);
                            string tickLabel = tickDate.ToString("yy-MM-dd");
                            Point tickCap = start;
                            tickCap.Offset(tickOffset, 0);

                            // draw major tick marks (with labels, if required)
                            this.DoDrawAxisTick(e.Graphics,
                                                linePen,
                                                tickCap,
                                                this.xAxis.MajorTick,
                                                true,
                                                false,
                                                this.xAxis.Labels.Visible ? tickLabel : null,
                                                this.xAxis.Labels.Rotation,
                                                labelBrush,
                                                this.xAxis.Labels.Font);
                        }
                    }

                    step = this.xAxis.Scale.MinorUnit;

                    if (step > 0 && max > min)
                    {
                        // loop to draw the minor tick marks (if needed)
                        if (this.xAxis.MinorTick != ChartTickMarkTypes.None)
                        {
                            for (int tick = min; tick <= max; tick += step)
                            {
                                // calculate tick's position on the axis
                                double proc = (double)(tick - min) / (max - min);
                                int tickOffset = (int)Math.Floor(proc * length);
                                Point tickCap = start;
                                tickCap.Offset(tickOffset, 0);

                                // draw minor tick marks
                                this.DoDrawAxisTick(e.Graphics,
                                                    linePen,
                                                    tickCap,
                                                    this.xAxis.MinorTick,
                                                    false,
                                                    false,
                                                    null,
                                                    this.xAxis.Labels.Rotation,
                                                    null,
                                                    null);
                            }
                        }
                    }
                }
                finally
                {
                    // free graphics resources
                    linePen.Dispose();
                    labelBrush.Dispose();
                }
            }
        }

        /// <summary>
        /// Raises the MeasureChartYAxisTitle event.
        /// </summary>
        /// <param name="e">A MeasureChartEventArgs object that contains the event data.</param>
        protected virtual void OnMeasureChartYAxisTitle(MeasureChartEventArgs e)
        {
            // check if there are subscribers
            if (this.MeasureChartYAxisTitle != null)
            {
                // raise the MeasureChartYAxisTitle event.
                this.MeasureChartYAxisTitle(this, e);
            }
        }

        /// <summary>
        /// Raises the DrawChartYAxisTitle event.
        /// </summary>
        /// <param name="e">A DrawChartEventArgs object that contains the event data.</param>
        protected virtual void OnDrawChartYAxisTitle(DrawChartEventArgs e)
        {
            // check if there are subscribers
            if (this.DrawChartYAxisTitle != null)
            {
                // raise the DrawChartYAxisTitle event.
                this.DrawChartYAxisTitle(this, e);
            }

            // check if there is text to draw
            if (e.Cancel == false && this.yAxis.Title.Text.Length != 0)
            {
                // initialize graphic resources
                Brush textBrush = new SolidBrush(this.yAxis.Title.Color);
                try
                {
                    // draw the value axis title
                    StringFormat format = CustomGraphics.StringFormat(ContentAlignment.MiddleCenter);
                    format.FormatFlags |= StringFormatFlags.DirectionVertical;
                    e.Graphics.DrawString(this.yAxis.Title.Text, this.yAxis.Title.Font, textBrush, e.Bounds, format);
                    format.FormatFlags &= ~StringFormatFlags.DirectionVertical;
                }
                finally
                {
                    // free graphics resources
                    textBrush.Dispose();
                }
            }
        }

        /// <summary>
        /// Raises the MeasureChartYAxis event.
        /// </summary>
        /// <param name="e">A MeasureChartEventArgs object that contains the event data.</param>
        protected virtual void OnMeasureChartYAxis(MeasureChartEventArgs e)
        {
            // check if there are subscribers
            if (this.MeasureChartYAxis != null)
            {
                // raise the MeasureChartYAxis event.
                this.MeasureChartYAxis(this, e);
            }
        }

        /// <summary>
        /// Raises the DrawChartYAxis event.
        /// </summary>
        /// <param name="e">A DrawChartEventArgs object that contains the event data.</param>
        protected virtual void OnDrawChartYAxis(DrawChartEventArgs e)
        {
            // check if there are subscribers
            if (this.DrawChartYAxis != null)
            {
                // raise the DrawChartYAxis event.
                this.DrawChartYAxis(this, e);
            }

            // check if we need draw
            if (e.Cancel == false && this.yAxis.Line.Visible && this.yAxis.Line.Weight > 0)
            {
                // determine chart's origin points
                Point start = e.Bounds.Location;
                start.Offset(0, 0);
                Point end = e.Bounds.Location;
                end.Offset(0, e.Bounds.Height - 0);

                // initialize graphic resources
                Pen linePen = this.yAxis.Line.ToPen();
                Brush labelBrush = new SolidBrush(this.yAxis.Labels.Color);
                try
                {
                    // draw the axis line
                    e.Graphics.DrawLine(linePen, start, end);

                    // initialize variable for tick marks drawing
                    int min = this.yAxis.Scale.Minimum;
                    int max = this.yAxis.Scale.Maximum;
                    int length = end.Y - start.Y;
                    int step = this.yAxis.Scale.MajorUnit;

                    if (step > 0 && max > min)
                    {
                        // loop to draw the major tick marks (if needed)
                        for (int tick = min; tick <= max; tick += step)
                        {
                            // calculate tick's position on the axis
                            double proc = (double)(tick - min) / (max - min);
                            int tickOffset = (int)Math.Floor(proc * length);
                            Point tickCap = end;
                            tickCap.Offset(0, -tickOffset);

                            // draw major tick marks (with labels, if required)
                            this.DoDrawAxisTick(e.Graphics,
                                                linePen,
                                                tickCap,
                                                this.yAxis.MajorTick,
                                                true,
                                                true,
                                                this.yAxis.Labels.Visible ? tick.ToString() : null,
                                                this.yAxis.Labels.Rotation,
                                                labelBrush,
                                                this.yAxis.Labels.Font);
                        }
                    }

                    step = this.yAxis.Scale.MinorUnit;

                    if (step > 0 && max > min)
                    {
                        // loop to draw the minor tick marks (if needed)
                        if (this.yAxis.MinorTick != ChartTickMarkTypes.None)
                        {
                            for (int tick = min; tick <= max; tick += step)
                            {
                                // calculate tick's position on the axis
                                double proc = (double)(tick - min) / (max - min);
                                int tickOffset = (int)Math.Floor(proc * length);
                                Point tickCap = end;
                                tickCap.Offset(0, -tickOffset);

                                // draw minor tick marks
                                this.DoDrawAxisTick(e.Graphics,
                                                    linePen,
                                                    tickCap,
                                                    this.yAxis.MinorTick,
                                                    false,
                                                    true,
                                                    null,
                                                    this.yAxis.Labels.Rotation,
                                                    null,
                                                    null);
                            }
                        }
                    }
                }
                finally
                {
                    // free graphics resources
                    linePen.Dispose();
                    labelBrush.Dispose();
                }
            }
        }

        /// <summary>
        /// Raises the MeasureChartGrid event.
        /// </summary>
        /// <param name="e">A MeasureChartEventArgs object that contains the event data.</param>
        protected virtual void OnMeasureChartGrid(MeasureChartEventArgs e)
        {
            // check if there are subscribers
            if (this.MeasureChartGrid != null)
            {
                // raise the MeasureChartGrid event.
                this.MeasureChartGrid(this, e);
            }
        }

        /// <summary>
        /// Raises the DrawChartGrid event.
        /// </summary>
        /// <param name="e">A DrawChartEventArgs object that contains the event data.</param>
        protected virtual void OnDrawChartGrid(DrawChartEventArgs e)
        {
            // check if there are subscribers
            if (this.DrawChartGrid != null)
            {
                // raise the DrawChartGrid event.
                this.DrawChartGrid(this, e);
            }

            // check if we need draw
            if (e.Cancel == false && this.grid.Line.Visible && this.grid.Line.Weight > 0)
            {
                // determine chart's origin points
                Point start = e.Bounds.Location;
                start.Offset(0, e.Bounds.Height);
                Point end = e.Bounds.Location;
                end.Offset(e.Bounds.Width, 0);

                // initialize graphic resources
                Pen linePen = this.grid.Line.ToPen();
                try
                {
                    // draw the grid lines for the time axis
                    if (this.grid.XAxisStyle != ChartGridStyles.None)
                    {
                        // init scale variables
                        DateTime minDate = this.xAxis.Scale.Minimum;
                        DateTime maxDate = this.xAxis.Scale.Maximum;
                        TimeSpan dateDiff = (maxDate - minDate);
                        int min = 0;
                        int max = dateDiff.Days;
                        int length = end.X - start.X;
                        // check if valid scale
                        if (max > min && length > 0)
                        {
                            switch (this.grid.XAxisStyle)
                            {
                                case ChartGridStyles.Major:
                                    if (this.xAxis.Scale.MajorUnit > 0)
                                    {
                                        // draw major grid lines for time axis
                                        this.DoDrawXGridLines(e.Graphics, linePen, start, end, min, max, length, this.xAxis.Scale.MajorUnit);
                                    }
                                    break;

                                case ChartGridStyles.Minor:
                                    if (this.xAxis.Scale.MinorUnit > 0)
                                    {
                                        // draw minor grid lines for time axis
                                        this.DoDrawXGridLines(e.Graphics, linePen, start, end, min, max, length, this.xAxis.Scale.MinorUnit);
                                    }
                                    break;
                            }
                        }
                    }

                    // draw the grid lines for the value axis
                    if (this.grid.YAxisStyle != ChartGridStyles.None)
                    {
                        // init scale variables
                        int min = this.yAxis.Scale.Minimum;
                        int max = this.yAxis.Scale.Maximum;
                        int length = start.Y - end.Y;
                        // check if valid scale
                        if (max > min && length > 0)
                        {
                            switch (this.grid.YAxisStyle)
                            {
                                case ChartGridStyles.Major:
                                    if (this.yAxis.Scale.MajorUnit > 0)
                                    {
                                        // draw major grid lines for value axis
                                        this.DoDrawYGridLines(e.Graphics, linePen, start, end, min, max, length, this.yAxis.Scale.MajorUnit);
                                    }
                                    break;

                                case ChartGridStyles.Minor:
                                    if (this.yAxis.Scale.MinorUnit > 0)
                                    {
                                        // draw minor grid lines for value axis
                                        this.DoDrawYGridLines(e.Graphics, linePen, start, end, min, max, length, this.yAxis.Scale.MinorUnit);
                                    }
                                    break;
                            }
                        }
                    }
                }
                finally
                {
                    // free graphics resources
                    linePen.Dispose();
                }
            }
        }

        /// <summary>
        /// Raises the MeasureChartLines event.
        /// </summary>
        /// <param name="e">A MeasureChartEventArgs object that contains the event data.</param>
        protected virtual void OnMeasureChartLines(MeasureChartEventArgs e)
        {
            // check if there are subscribers
            if (this.MeasureChartLines != null)
            {
                // raise the MeasureChartLines event.
                this.MeasureChartLines(this, e);
            }
        }

        /// <summary>
        /// Raises the DrawChartLines event.
        /// </summary>
        /// <param name="e">A DrawChartEventArgs object that contains the event data.</param>
        protected virtual void OnDrawChartLines(DrawChartEventArgs e)
        {
            // check if there are subscribers
            if (this.DrawChartLines != null)
            {
                // raise the DrawChartLines event.
                this.DrawChartLines(this, e);
            }

            // check if we need draw
            if (e.Cancel == false && this.series.Values.Count > 0)
            {
                // calculate new clipping rectangle
                Rectangle clipRect = e.Bounds;
                clipRect.Inflate(LARGE_TICK_SIZE, LARGE_TICK_SIZE);

                // reset clipping region
                Region oldClip = e.Graphics.Clip;
                e.Graphics.Clip = new Region(clipRect);
                try
                {
                    // determine coordinates
                    Point start = e.Bounds.Location;
                    start.Offset(0, e.Bounds.Height);
                    Point end = e.Bounds.Location;
                    end.Offset(e.Bounds.Width, 0);

                    // calculate parameters for X scale
                    DateTime minDate = this.xAxis.Scale.Minimum;
                    DateTime maxDate = this.xAxis.Scale.Maximum;
                    TimeSpan dateDiff = (maxDate - minDate);
                    int xMin = 0;
                    int xMax = dateDiff.Days;
                    int xLen = end.X - start.X;

                    // calculate parameters for Y scale
                    double yMin = this.yAxis.Scale.Minimum;
                    double yMax = this.yAxis.Scale.Maximum;
                    double yLen = start.Y - end.Y;

                    // check if we have something to draw
                    if (xMax > xMin && yMax > yMin)
                    {
                        // initialize graphics resources
                        Pen linePen = this.series.Line.ToPen();
                        Pen borderPen = new Pen(this.series.Mark.BorderColor);
                        Brush fillBrush = new SolidBrush(this.series.Mark.FillColor);
                        Brush labelBrush = new SolidBrush(this.series.Labels.Color);
                        Pen projLine = this.series.Projections.Line.ToPen();
                        Brush projBrush = new SolidBrush(this.series.Projections.Labels.Color);
                        try
                        {
                            // declare residual mark
                            Point prev = Point.Empty;
                            string dateLabel = String.Empty;
                            string valueLabel = String.Empty;

                            // sort list by date (bubble sort)
                            int count = this.series.Values.Count;
                            ChartSeriesValue[] sortedValues = new ChartSeriesValue[count];
                            this.series.Values.CopyTo(sortedValues);

                            // actual sorting loop (bubble sort)
                            for (int bubble = 0; bubble < count; bubble++)
                            {
                                for (int lookup = bubble + 1; lookup < count; lookup++)
                                {
                                    if (sortedValues[bubble].Date < sortedValues[lookup].Date)
                                    {
                                        ChartSeriesValue temp = sortedValues[bubble];
                                        sortedValues[bubble] = sortedValues[lookup];
                                        sortedValues[lookup] = temp;
                                    }
                                }
                            }

                            // draw chart line
                            foreach (ChartSeriesValue seriesValue in sortedValues)
                            {
                                // determine position on time scale
                                dateDiff = seriesValue.Date - minDate;
                                int xPos = dateDiff.Days;

                                double xProc = (double)(xPos - xMin) / (xMax - xMin);
                                int xValue = start.X + (int)Math.Floor(xProc * xLen);

                                // determine position on value scale
                                double yProc = ((double)seriesValue.Value - yMin) / (yMax - yMin);
                                int yValue = start.Y - (int)Math.Floor(yProc * yLen);

                                Point crt = new Point(xValue, yValue);
                                if (prev.IsEmpty == false)
                                {
                                    // draw chart line
                                    e.Graphics.DrawLine(linePen, prev, crt);
                                    // draw mark projection
                                    this.DoDrawChartProjection(e.Graphics,
                                                                projLine,
                                                                projBrush,
                                                                this.series.Projections.Labels.Font,
                                                                prev,
                                                                start.Y,
                                                                this.series.Projections.Labels.Rotation,
                                                                dateLabel);
                                    // draw current mark
                                    this.DoDrawChartMark(e.Graphics,
                                                            borderPen,
                                                            fillBrush,
                                                            prev,
                                                            this.series.Mark.Shape,
                                                            Chart.MARK_SIZE);
                                    // check if series labels are requiered
                                    if (this.series.Labels.Visible)
                                    {
                                        // draw the label for current mark
                                        this.DoDrawChartMarkLabel(e.Graphics,
                                                                    labelBrush,
                                                                    this.series.Labels.Font,
                                                                    prev,
                                                                    Chart.MARK_SIZE,
                                                                    this.series.Labels.Rotation,
                                                                    valueLabel);
                                    }
                                }

                                // store value for use in the next loop
                                dateLabel = seriesValue.Date.ToString("d/MM");
                                valueLabel = seriesValue.Value.ToString();
                                prev = crt;
                            }

                            // end the chart (use the values from the last loop from previos foreach statement)
                            if (prev.IsEmpty == false)
                            {
                                // draw mark projection
                                this.DoDrawChartProjection(e.Graphics,
                                                            projLine,
                                                            projBrush,
                                                            this.series.Projections.Labels.Font,
                                                            prev,
                                                            start.Y,
                                                            this.series.Projections.Labels.Rotation,
                                                            dateLabel);
                                // draw residual mark
                                this.DoDrawChartMark(e.Graphics,
                                                        borderPen,
                                                        fillBrush,
                                                        prev,
                                                        this.series.Mark.Shape,
                                                        Chart.MARK_SIZE);
                                // check if series labels are requiered
                                if (this.series.Labels.Visible)
                                {
                                    // draw label for residual mark
                                    this.DoDrawChartMarkLabel(e.Graphics,
                                                                labelBrush,
                                                                this.series.Labels.Font,
                                                                prev,
                                                                Chart.MARK_SIZE,
                                                                this.series.Labels.Rotation,
                                                                valueLabel);
                                }
                            }
                        }
                        finally
                        {
                            // free graphics resources
                            linePen.Dispose();
                            borderPen.Dispose();
                            fillBrush.Dispose();
                            labelBrush.Dispose();
                            projLine.Dispose();
                            projBrush.Dispose();
                        }
                    }
                }
                finally
                {
                    // restore old clipping region 
                    e.Graphics.Clip = oldClip;
                }
            }
        }

        /// <summary>
        /// Draws the grid lines for the time axis.
        /// </summary>
        private void DoDrawXGridLines(Graphics g,
                                        Pen pen,
                                        Point start,
                                        Point end,
                                        int min,
                                        int max,
                                        int length,
                                        int step)
        {
            // get the width of the axis
            int axisWidth = (int)this.yAxis.Line.Weight;

            // draw all grid lines
            for (int line = min + step; line <= max; line += step)
            {
                // calculate the offset from the chart's origin
                double proc = (double)(line - min) / (max - min);
                int gridOffset = (int)Math.Floor(proc * length);

                // determine grid lines caps
                Point gridCap = start;
                gridCap.Offset(gridOffset, -axisWidth);
                Point gridCap2 = end;
                gridCap2.Offset(gridOffset - length, 0);

                // draw the grid line
                g.DrawLine(pen, gridCap, gridCap2);
            }
        }

        /// <summary>
        /// Draws the grid lines for the value axis.
        /// </summary>
        private void DoDrawYGridLines(Graphics g,
                                        Pen pen,
                                        Point start,
                                        Point end,
                                        int min,
                                        int max,
                                        int length,
                                        int step)
        {
            // get the width of the axis
            int axisWidth = (int)this.xAxis.Line.Weight;

            // draw all grid lines
            for (int line = min + step; line <= max; line += step)
            {
                // calculate the offset from the chart's origin
                double proc = (double)(line - min) / (max - min);
                int gridOffset = (int)Math.Floor(proc * length);

                // determine grid lines caps
                Point gridCap = start;
                gridCap.Offset(axisWidth, -gridOffset);
                Point gridCap2 = end;
                gridCap2.Offset(0, length - gridOffset);

                // draw the grid line
                g.DrawLine(pen, gridCap, gridCap2);
            }
        }

        /// <summary>
        /// Draws the tick marks for time or values axis.
        /// </summary>
        private void DoDrawAxisTick(Graphics g,
                                    Pen pen,
                                    Point point,
                                    ChartTickMarkTypes tickType,
                                    bool major,
                                    bool vertical,
                                    string label,
                                    float rotation,
                                    Brush labelBrush,
                                    Font labelFont)
        {
            // determine tick size
            int offset = major ? LARGE_TICK_SIZE : SMALL_TICK_SIZE;

            // declare tick line caps
            Point tickCapIn = point;
            Point tickCapOut = point;

            // determine tick line caps (based on tick type settings)
            switch (tickType)
            {
                case ChartTickMarkTypes.Outside:
                    if (vertical)
                    {
                        tickCapOut.Offset(-offset, 0);
                    }
                    else
                    {
                        tickCapOut.Offset(0, offset);
                    }
                    break;

                case ChartTickMarkTypes.Inside:
                    if (vertical)
                    {
                        tickCapIn.Offset(offset, 0);
                    }
                    else
                    {
                        tickCapIn.Offset(0, -offset);
                    }
                    break;

                case ChartTickMarkTypes.Cross:
                    if (vertical)
                    {
                        tickCapIn.Offset(offset, 0);
                        tickCapOut.Offset(-offset, 0);
                    }
                    else
                    {
                        tickCapIn.Offset(0, -offset);
                        tickCapOut.Offset(0, offset);
                    }
                    break;
            }

            // check if we need to draw the tick mark
            if (this.yAxis.MajorTick != ChartTickMarkTypes.None)
            {
                g.DrawLine(pen, tickCapIn, tickCapOut);
            }

            // draw tick mark label (if required)
            if (label != null)
            {
                StringFormat format = CustomGraphics.StringFormat(vertical ? ContentAlignment.MiddleRight : ContentAlignment.TopCenter);
                if (rotation != 0.0F)
                {
                    g.TranslateTransform(tickCapOut.X, tickCapOut.Y);
                    g.RotateTransform(rotation);
                    g.DrawString(label, labelFont, labelBrush, Point.Empty, format);
                    g.ResetTransform();
                }
                else
                {
                    g.DrawString(label, labelFont, labelBrush, tickCapOut, format);
                }
            }
        }

        /// <summary>
        /// Draws the projection from a chart series mark to the time axis.
        /// </summary>
        private void DoDrawChartProjection(Graphics g,
                                            Pen linePen,
                                            Brush labelBrush,
                                            Font labelFont,
                                            Point chartPoint,
                                            int yAxisOrigin,
                                            float rotation,
                                            string label)
        {
            // get the width of the axis
            int axisWidth = (int)this.xAxis.Line.Weight;

            // check if we need to draw projections
            if (this.series.Projections.Visible && this.series.Projections.Line.Weight > 0)
            {
                // determine projection
                Point projection = chartPoint;
                projection.Y = yAxisOrigin - axisWidth;

                // draw projection line
                g.DrawLine(linePen, projection, chartPoint);

                // draw projection label (if required)
                if (this.series.Projections.Labels.Visible)
                {
                    if (rotation != 0.0F)
                    {
                        g.TranslateTransform(projection.X, projection.Y);
                        g.RotateTransform(rotation);
                        StringFormat format = CustomGraphics.StringFormat(ContentAlignment.MiddleLeft);
                        g.DrawString(label, labelFont, labelBrush, Point.Empty, format);
                        g.ResetTransform();
                    }
                    else
                    {
                        StringFormat format = CustomGraphics.StringFormat(ContentAlignment.BottomLeft);
                        g.DrawString(label, labelFont, labelBrush, projection, format);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the chart series mark using the specified shape.
        /// </summary>
        private void DoDrawChartMark(Graphics g,
                                    Pen borderPen,
                                    Brush fillBrush,
                                    Point markPoint,
                                    ChartMarkShapes shape,
                                    int markSize)
        {
            switch (shape)
            {
                case ChartMarkShapes.Circle:
                    Rectangle circle = new Rectangle(markPoint.X - markSize,
                                                    markPoint.Y - markSize,
                                                    markSize * 2,
                                                    markSize * 2);
                    g.FillEllipse(fillBrush, circle);
                    g.DrawEllipse(borderPen, circle);
                    break;

                case ChartMarkShapes.Diamond:
                    Point[] diamond = new Point[4];

                    diamond[0] = markPoint;
                    diamond[0].Offset(0, markSize);
                    diamond[1] = markPoint;
                    diamond[1].Offset(markSize, 0);
                    diamond[2] = markPoint;
                    diamond[2].Offset(0, -markSize);
                    diamond[3] = markPoint;
                    diamond[3].Offset(-markSize, 0);

                    g.FillPolygon(fillBrush, diamond);
                    g.DrawPolygon(borderPen, diamond);
                    break;

                case ChartMarkShapes.Square:
                    Rectangle square = new Rectangle(markPoint.X - markSize,
                                                    markPoint.Y - markSize,
                                                    markSize * 2,
                                                    markSize * 2);
                    g.FillRectangle(fillBrush, square);
                    g.DrawRectangle(borderPen, square);
                    break;

                case ChartMarkShapes.Triangle:
                    Point[] triangle = new Point[3];

                    triangle[0] = markPoint;
                    triangle[0].Offset(0, -markSize);
                    triangle[1] = markPoint;
                    triangle[1].Offset(markSize, markSize);
                    triangle[2] = markPoint;
                    triangle[2].Offset(-markSize, markSize);

                    g.FillPolygon(fillBrush, triangle);
                    g.DrawPolygon(borderPen, triangle);
                    break;
            }
        }

        /// <summary>
        /// Draws the chart series mark label if required.
        /// </summary>
        private void DoDrawChartMarkLabel(Graphics g,
                                            Brush labelBrush,
                                            Font labelFont,
                                            Point markPoint,
                                            int markSize,
                                            float rotation,
                                            string label)
        {
            // determine label position relative to mark position
            Point labelPos = markPoint;
            labelPos.Offset(markSize, -markSize);

            // draw the label besides the mark
            StringFormat format = CustomGraphics.StringFormat(ContentAlignment.BottomCenter);
            if (rotation != 0.0F)
            {
                g.TranslateTransform(labelPos.X, labelPos.Y);
                g.RotateTransform(rotation);
                g.DrawString(label, labelFont, labelBrush, Point.Empty, format);
                g.ResetTransform();
            }
            else
            {
                g.DrawString(label, labelFont, labelBrush, labelPos, format);
            }
        }

        /// <summary>
        /// Determines the size of the chart legend item.
        /// </summary>
        /// <returns>A Size object representing the size of the chart legend item.</returns>
        private Size GetLegendItemSize(Graphics g)
        {
            // determine the size of the title of the series
            SizeF stringSize = g.MeasureString(this.series.Title.Text, this.series.Title.Font, 0, StringFormat.GenericTypographic);

            // determine the legend item's width
            float maxEstate = (float)Math.Ceiling(LEGEND_MAX_WIDTH * this.Width);
            int itemWidth = (int)Math.Min(maxEstate, Math.Ceiling(stringSize.Width));

            // determine the legend item's width
            int itemHeight = Math.Max(Chart.MARK_SIZE, (int)Math.Ceiling(stringSize.Height));

            // return the computed size of the legend item
            return new Size(itemWidth, itemHeight);
        }

        #endregion

        #region Events
        /// <summary>
        /// Occurs before the chart title is about to be drawn.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs before the chart title is about to be drawn.")]
        public event DrawChartEventHandler DrawChartTitle;
        /// <summary>
        /// Occurs after the area where chart title will be drawn is measured.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs after the area where chart title will be drawn is measured.")]
        public event MeasureChartEventHandler MeasureChartTitle;
        /// <summary>
        /// Occurs before the chart legend is about to be drawn.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs before the chart legend is about to be drawn.")]
        public event DrawChartEventHandler DrawChartLegend;
        /// <summary>
        /// Occurs after the area where chart legend will be drawn is measured.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs after the area where chart legend will be drawn is measured.")]
        public event MeasureChartEventHandler MeasureChartLegend;
        /// <summary>
        /// Occurs before the chart X axis is about to be drawn.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs before the chart time axis is about to be drawn.")]
        public event DrawChartEventHandler DrawChartXAxis;
        /// <summary>
        /// Occurs after the area where chart X axis will be drawn is measured.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs after the area where chart time axis will be drawn is measured.")]
        public event MeasureChartEventHandler MeasureChartXAxis;
        /// <summary>
        /// Occurs before the chart time axis title is about to be drawn.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs before the chart time axis title is about to be drawn.")]
        public event DrawChartEventHandler DrawChartXAxisTitle;
        /// <summary>
        /// Occurs after the area where chart time axis title will be drawn is measured.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs after the area where chart time axis title will be drawn is measured.")]
        public event MeasureChartEventHandler MeasureChartXAxisTitle;
        /// <summary>
        /// Occurs before the chart values axis is about to be drawn.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs before the chart values axis is about to be drawn.")]
        public event DrawChartEventHandler DrawChartYAxis;
        /// <summary>
        /// Occurs after the area where chart values axis will be drawn is measured.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs after the area where chart values axis will be drawn is measured.")]
        public event MeasureChartEventHandler MeasureChartYAxis;
        /// <summary>
        /// Occurs before the chart values axis title is about to be drawn.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs before the chart values axis title is about to be drawn.")]
        public event DrawChartEventHandler DrawChartYAxisTitle;
        /// <summary>
        /// Occurs after the area where chart values axis title will be drawn is measured.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs after the area where chart values axis title will be drawn is measured.")]
        public event MeasureChartEventHandler MeasureChartYAxisTitle;
        /// <summary>
        /// Occurs before the chart grid is about to be drawn.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs before the chart grid is about to be drawn.")]
        public event DrawChartEventHandler DrawChartGrid;
        /// <summary>
        /// Occurs after the area where chart grid will be drawn is measured.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs after the area where chart grid will be drawn is measured.")]
        public event MeasureChartEventHandler MeasureChartGrid;
        /// <summary>
        /// Occurs before the chart lines is about to be drawn.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs before the chart lines is about to be drawn.")]
        public event DrawChartEventHandler DrawChartLines;
        /// <summary>
        /// Occurs after the area where chart lines will be drawn is measured.
        /// </summary>
        [Category("Drawing")]
        [Description("Occurs after the area where chart lines will be drawn is measured.")]
        public event MeasureChartEventHandler MeasureChartLines;
        #endregion
    }

    #region MeasureChart event declarations
    /// <summary>
    /// Provides data for the MeasureChart event.  
    /// </summary>
    public class MeasureChartEventArgs : EventArgs
    {
        #region Fields
        private Graphics graphics;
        private Rectangle bounds;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of class MeasureChartEventArgs.
        /// </summary>
        /// <param name="graphics">A Graphics object used to perform drawing on the chart.</param>
        /// <param name="bounds">A Rectangle object representing the bounding rectangle of the item beeng drawn.</param>
        public MeasureChartEventArgs(Graphics graphics, Rectangle bounds)
        {
            this.graphics = graphics;
            this.bounds = bounds;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Represents the Graphics object used to perform drawing on the chart.
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        /// <summary>
        /// Represents the bounding rectangle of the item beeng drawn.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
            set
            {
                this.bounds = value;
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents the method that will handle the MeasureChart event of a Chart.  
    /// </summary>
    public delegate void MeasureChartEventHandler(object sender, MeasureChartEventArgs e);
    #endregion

    #region DrawChart event declarations
    /// <summary>
    /// Provides data for the DrawChart event.  
    /// </summary>
    public class DrawChartEventArgs : EventArgs
    {
        #region Fields
        private bool cancel = false;
        private Graphics graphics;
        private MeasureChartEventArgs measure;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of class DrawChartEventArgs.
        /// </summary>
        /// <param name="graphics">A Graphics object used to perform drawing on the chart.</param>
        /// <param name="measure">A MeasureChartEventArgs object representing the measures used to perform drawing of current item.</param>
        public DrawChartEventArgs(Graphics graphics, MeasureChartEventArgs measure)
        {
            this.graphics = graphics;
            this.measure = measure;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Allows the user to cancel the default drawing of the current item.
        /// </summary>
        public bool Cancel
        {
            get
            {
                return this.cancel;
            }
            set
            {
                this.cancel = value;
            }

        }

        /// <summary>
        /// Represents the Graphics object used to perform drawing on the chart.
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        /// <summary>
        /// Represents the bounding rectangle of current item.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return this.measure.Bounds;
            }
        }

        /// <summary>
        /// Represents the measures used to perform drawing of current item.
        /// </summary>
        public MeasureChartEventArgs MeasureEventArgs
        {
            get
            {
                return this.measure;
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents the method that will handle the DrawChart event of a chart.  
    /// </summary>
    public delegate void DrawChartEventHandler(object sender, DrawChartEventArgs e);
    #endregion
}
