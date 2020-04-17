//Ninjatrader 8 C# code for saving price/time values (coordinates) of each mouse right click on the chart.
//This is a tool developed to help backtesting patterns in the any market.
//Instead of typing dates and prices to Excel spreadsheet this will save data to .txt file seperated with commas.
//Once ready, open .txt and import to Excel .csv using special Paste with comma separated text.
//Super easy to use and greatly increases backtesting speed, efficiency and data correctness.

#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion


// Add this to your declarations to use StreamWriter
using System.IO;

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class chartclicker : Indicator
	{
		
		private ChartScale						chartScale;
		private Point							clickPoint	= new Point();
		private double							convertedPrice;
		private DateTime						convertedTime;
		private string path;
		private StreamWriter sw; // a variable for the StreamWriter that will be used
		private int counter = 0;
		private int patterncount = 0;
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description							= @"Demonstrates how to capture a mouse click and adjust for DPI settings properly";
				Name								= "chartclicker";
				Calculate							= Calculate.OnBarClose;
				IsOverlay							= true;
				DisplayInDataBox					= false;
				DrawOnPricePanel					= true;
				ScaleJustification					= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				path 								= NinjaTrader.Core.Globals.UserDataDir + "BACKTESTFILE_KK.txt"; // Define the Path to our test file
				
			}
			else if (State == State.Historical)
			{
				if (ChartControl != null)
				{
					foreach (ChartScale scale in ChartPanel.Scales)
						if (scale.ScaleJustification == ScaleJustification)
							chartScale = scale;

					ChartControl.MouseLeftButtonDown += MouseClicked;
				}
			}
			else if (State == State.Terminated)
			{
				if (ChartControl != null)
					ChartControl.MouseLeftButtonDown -= MouseClicked;
			}
				if (sw != null)
			{
					sw.Close();
					sw.Dispose();
					sw = null;
			}
		}

		protected override void OnBarUpdate() {	
			

		}
		
				
		protected void MouseClicked(object sender, MouseButtonEventArgs e)
			
		{
			
			clickPoint.X = ChartingExtensions.ConvertToHorizontalPixels(e.GetPosition(ChartControl as IInputElement).X, ChartControl.PresentationSource);
			clickPoint.Y = ChartingExtensions.ConvertToVerticalPixels(e.GetPosition(ChartControl as IInputElement).Y, ChartControl.PresentationSource);

			convertedPrice	= Instrument.MasterInstrument.RoundToTickSize(chartScale.GetValueByY((float)clickPoint.Y));

			convertedTime	= ChartControl.GetTimeBySlotIndex((int)ChartControl.GetSlotIndexByX((int)clickPoint.X));
			

			if (counter == 0)
			{
				++counter;
				++patterncount;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write("New Pattern,");
				Draw.TextFixed(this, "priceTime", "Click to Start", TextPosition.BottomLeft, ChartControl.Properties.ChartText, 
  				ChartControl.Properties.LabelFont, Brushes.LimeGreen, Brushes.Transparent, 0);
				sw.Close(); // Close the file to allow future calls to access the file again.
				Print("NEW PATTERN");
			}
			
			else if (counter == 1)
			{
				++counter;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write(string.Format("{0},", convertedPrice)); // Append a new line to the file
				Draw.TextFixed(this, "priceTime", string.Format("Click on X Point"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.	
				Print(string.Format("{0},", convertedPrice));
			}
			
			else if (counter == 2)
			{
				++counter;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write(string.Format("{0},", convertedPrice)); // Append a new line to the file
				Draw.TextFixed(this, "priceTime", string.Format("Click on A Point"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.	
				Print(string.Format("{0},", convertedPrice));
			}
			
			else if (counter == 3)
			{
				++counter;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write(string.Format("{0},", convertedPrice)); // Append a new line to the file
				Draw.TextFixed(this, "priceTime", string.Format("Click on C Point"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.	
				Print(string.Format("{0},", convertedPrice));
			}
					
			else if (counter == 4)
			{
				++counter;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write(string.Format("{0},", convertedPrice)); // Append a new line to the file
				Draw.TextFixed(this, "priceTime", string.Format("Click on D/ENTRY Point"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.
				Print(string.Format("{0},", convertedPrice));
			}
								
			else if (counter == 5)
			{
				++counter;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write(string.Format("{0},", convertedTime)); // Append a new line to the file
				Draw.TextFixed(this, "priceTime", string.Format("Click on D/ENTRY Date"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.
				Print(string.Format("{0},", convertedPrice));
			}
								
			else if (counter == 6)
			{
				++counter;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write(string.Format("{0},", convertedPrice)); // Append a new line to the file
				Draw.TextFixed(this, "priceTime", string.Format("Click on TARGET 1"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.
				Print(string.Format("{0},", convertedPrice));
			}
				
			else if (counter == 7)
			{
				++counter;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write(string.Format("{0},", convertedPrice)); // Append a new line to the file
				Draw.TextFixed(this, "priceTime", string.Format("Click on MAE"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.
				Print(string.Format("{0},", convertedPrice));
			}
								
			else if (counter == 8)
			{
				++counter;
				sw = File.AppendText(path);  // Open the path for writing
				sw.Write(string.Format("{0},", convertedTime)); // Append a new line to the file
				Draw.TextFixed(this, "priceTime", string.Format("Click on Close Date/Time"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.
				Print(string.Format("{0},", convertedPrice));
			}
		
			
			else if(counter == 9)
			{
				counter = 0;
				sw = File.AppendText(path);  // Open the path for writing
				sw.WriteLine("");
				Draw.TextFixed(this, "priceTime", string.Format("Pattern Complete"), TextPosition.BottomLeft);
				sw.Close(); // Close the file to allow future calls to access the file again.
				Print("PATTERN COMPLETE");
			}
			
			
			
			


			ForceRefresh();
		}
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private chartclicker[] cachechartclicker;
		public chartclicker chartclicker()
		{
			return chartclicker(Input);
		}

		public chartclicker chartclicker(ISeries<double> input)
		{
			if (cachechartclicker != null)
				for (int idx = 0; idx < cachechartclicker.Length; idx++)
					if (cachechartclicker[idx] != null &&  cachechartclicker[idx].EqualsInput(input))
						return cachechartclicker[idx];
			return CacheIndicator<chartclicker>(new chartclicker(), input, ref cachechartclicker);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.chartclicker chartclicker()
		{
			return indicator.chartclicker(Input);
		}

		public Indicators.chartclicker chartclicker(ISeries<double> input )
		{
			return indicator.chartclicker(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.chartclicker chartclicker()
		{
			return indicator.chartclicker(Input);
		}

		public Indicators.chartclicker chartclicker(ISeries<double> input )
		{
			return indicator.chartclicker(input);
		}
	}
}

#endregion
