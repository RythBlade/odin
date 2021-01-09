using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace physics_debugger.Controls.Graph
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GraphChannel
    {
        public enum Axis
        {
            Hidden
            , PrimaryAxis
            , SecondaryAxis
        }

        public string Name { get; set; } = string.Empty;

        private Axis displayAxis = Axis.Hidden;
        public Axis DisplayAxis
        {
            get { return displayAxis; }
            set
            {
                if (displayAxis != value)
                {
                    // if the axis is currently hidden - re-add it
                    if (displayAxis == Axis.Hidden)
                    {
                        TargetChart.Series.Add(dataSeries);
                    }

                    switch (value)
                    {
                        case Axis.Hidden:
                            TargetChart.Series.Remove(dataSeries);
                            break;
                        case Axis.PrimaryAxis:
                            dataSeries.YAxisType = AxisType.Primary;
                            break;
                        case Axis.SecondaryAxis:
                        default:
                            dataSeries.YAxisType = AxisType.Secondary;
                            break;
                    }

                    displayAxis = value;
                }
            }
        }

        public List<DataPoint> DataPoints { get; } = new List<DataPoint>();

        public Chart TargetChart { get; } = null;

        private Series dataSeries = new Series();

        public GraphChannel(string name, Chart targetChart, List<DataPoint> dataPoints, Axis displayAxis)
        {
            Name = name;
            TargetChart = targetChart;
            DataPoints = dataPoints;

            dataSeries.ChartType = SeriesChartType.Line;
            dataSeries.LegendText = Name;

            foreach (DataPoint point in DataPoints)
            {
                dataSeries.Points.Add(point);
            }

            // set the display axis last so it updates everything
            DisplayAxis = displayAxis;
        }
    }
}
