using System;
using System.Collections.Generic;

namespace tivBudget.Dal.SimpleModels
{
  public class ChartConfigModel
  {
    public string Type { get; set; }
    public DataConfig Data { get; set; } = new DataConfig();
    public ChartOptions Options { get; set; } = new ChartOptions();
  }

  public class DataConfig
  {
    public List<string> Labels { get; set; } = new List<string>();
    public List<Series> DataSets { get; set; } = new List<Series>();
  }

  public class ChartOptions
  {
    public PluginOptions Plugins { get; set; } = new PluginOptions();
    public bool Responsive { get; set; }
    public ScaleOptions Scales { get; set; } = new ScaleOptions();
  }

  public class PluginOptions
  {
    public Title Title { get; set; }
  }

  public class ScaleOptions
  {
    public ScaleOption X { get; set; } = new ScaleOption();
    public ScaleOption Y { get; set; } = new ScaleOption();
  }

  public class ScaleOption
  {
    public bool Stacked { get; set; }
  }

  public class Series
  {
    public string Label { get; set; }
    public List<Double> Data { get; set; } = new List<Double>();
  }

  public class Title
  {
    public bool Display { get; set; }
    public string Text { get; set; }
  }
}