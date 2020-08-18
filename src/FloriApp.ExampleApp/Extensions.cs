using System;
using System.Collections.Generic;
using System.Text;
using FloriApp.WebApi.Models;

namespace FloriApp.ExampleApp
{
  public static class Extensions
  {

    public static void Add(this List<Feature> features, string key, string value)
    {
      features.Add(new Feature()
      {
        Key = key,
        Value = value
      });
    }
  }
}
