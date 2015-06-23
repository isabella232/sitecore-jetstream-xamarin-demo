namespace Jetstream.Font
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Android.Graphics;
  using Com.Mikepenz.Iconics.Typeface;

  public class JetstreamIcons : Java.Lang.Object, ITypeface
  {
    const string TtfFile = "jetstream_design.ttf";

    static Typeface _typeface = null;

    static Dictionary<string, Java.Lang.Character> _chars;

    public IIcon GetIcon(string key)
    {
      return Icon.Values.ToList().Find(icon => icon.Name.Equals(key));
    }

    public IDictionary<string, Java.Lang.Character> Characters
    {
      get
      {
        return _chars ?? (_chars = Icon.Values.ToDictionary(icon => icon.Name, icon => new Java.Lang.Character(icon.Character)));
      }
    }

    public Typeface GetTypeface(Android.Content.Context context)
    {
      if (_typeface == null)
      {
        try
        {
          _typeface = Typeface.CreateFromAsset(context.Assets, "fonts/" + TtfFile);
        }
        catch (Exception e)
        {
          AppLog.Logger.Error("Failed to load font from Assets: " + e.Message);
        }
      }
      return _typeface;
    }

    public string Author
    {
      get
      {
        return "Sitecore";
      }
    }

    public string Description
    {
      get
      {
        return "Jetstream's iconfont";
      }
    }

    public string FontName
    {
      get
      {
        return "JetstreamIcons";
      }
    }

    public int IconCount
    {
      get
      {
        return _chars.Count;
      }
    }

    public ICollection<string> Icons
    {
      get
      {
        return Icon.Values.Select(icon => icon.Name).ToList();
      }
    }

    public string License
    {
      get
      {
        return "";
      }
    }

    public string LicenseUrl
    {
      get
      {
        return "";
      }
    }

    public string MappingPrefix
    {
      get
      {
        return "jet";
      }
    }

    public string Url
    {
      get
      {
        return "";
      }
    }

    public string Version
    {
      get
      {
        return "1.0.0";
      }
    }

    public class Icon : Java.Lang.Object, IIcon
    {
      private readonly char character;
      private readonly string name;

      private static ITypeface _typeFace;

      public Icon(char character, string name)
      {
        this.character = character;
        this.name = name;
      }

      public char Character
      {
        get
        {
          return this.character;
        }
      }

      public string FormattedName
      {
        get
        {
          return "{" + this.name + "}";
        }
      }

      public string Name
      {
        get
        {
          return this.name;
        }
      }

      public ITypeface Typeface
      {
        get
        {
          return _typeFace ?? (_typeFace = new JetstreamIcons());
        }
      }

      public static readonly Icon About = new Icon('\ue600', "about");
      public static readonly Icon Destinations = new Icon('\ue601', "destinations");
      public static readonly Icon FlightStatus = new Icon('\ue602', "flight_status");
      public static readonly Icon OnlineCheckin = new Icon('\ue603', "online_checkin");
      public static readonly Icon Profile = new Icon('\ue604', "profile");
      public static readonly Icon Settings = new Icon('\ue605', "settings");

      public static IEnumerable<Icon> Values
      {
        get
        {
          yield return About;
          yield return Destinations;
          yield return FlightStatus;
          yield return OnlineCheckin;
          yield return Profile;
          yield return Settings;
        }
      }
    }
  }
}
