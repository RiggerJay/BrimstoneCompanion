namespace RedSpartan.BrimstoneCompanion.MauiUI.Controls;

public partial class MouseView : Grid
{
    public MouseView()
    {
        InitializeComponent();
    }

    #region TitleText Property

    public string TitleText
    {
        get { return (string)GetValue(TitleTextProperty); }
        set { SetValue(TitleTextProperty, value); }
    }

    public static readonly BindableProperty TitleTextProperty =
        BindableProperty.Create(nameof(TitleText), typeof(string), typeof(MouseView), default);

    #endregion TitleText Property

    #region TitleTextSize Property

    public double TitleTextSize
    {
        get { return (double)GetValue(TitleTextSizeProperty); }
        set { SetValue(TitleTextSizeProperty, value); }
    }

    public static readonly BindableProperty TitleTextSizeProperty =
        BindableProperty.Create(nameof(TitleTextSize), typeof(double), typeof(MouseView), (double)14);

    #endregion TitleTextSize Property

    #region MainValue Property

    public string MainValue
    {
        get { return (string)GetValue(MainValueProperty); }
        set { SetValue(MainValueProperty, value); }
    }

    public static readonly BindableProperty MainValueProperty =
        BindableProperty.Create(nameof(MainValue), typeof(string), typeof(MouseView), default);

    #endregion MainValue Property

    #region MainFontSize Property

    public double MainFontSize
    {
        get { return (double)GetValue(MainFontSizeProperty); }
        set { SetValue(MainFontSizeProperty, value); }
    }

    public static readonly BindableProperty MainFontSizeProperty =
        BindableProperty.Create(nameof(MainFontSize), typeof(double), typeof(MouseView), (double)30);

    #endregion MainFontSize Property

    #region LeftValue Property

    public string LeftValue
    {
        get { return (string)GetValue(LeftValueProperty); }
        set { SetValue(LeftValueProperty, value); }
    }

    public static readonly BindableProperty LeftValueProperty =
        BindableProperty.Create(nameof(LeftValue), typeof(string), typeof(MouseView), default);

    #endregion LeftValue Property

    #region RightValue Property

    public string RightValue
    {
        get { return (string)GetValue(RightValueProperty); }
        set { SetValue(RightValueProperty, value); }
    }

    public static readonly BindableProperty RightValueProperty =
        BindableProperty.Create(nameof(RightValue), typeof(string), typeof(MouseView), default);

    #endregion RightValue Property

    #region MiniValueSize Property

    public double MiniValueSize
    {
        get { return (double)GetValue(MiniValueSizeProperty); }
        set { SetValue(MiniValueSizeProperty, value); }
    }

    public static readonly BindableProperty MiniValueSizeProperty =
        BindableProperty.Create(nameof(MiniValueSize), typeof(double), typeof(MouseView), (double)10);

    #endregion MiniValueSize Property

    #region IsCircle Property

    public bool IsCircle
    {
        get { return (bool)GetValue(IsCircleProperty); }
        set { SetValue(IsCircleProperty, value); }
    }

    public static readonly BindableProperty IsCircleProperty =
        BindableProperty.Create(nameof(IsCircle), typeof(bool), typeof(MouseView), false);

    #endregion IsCircle Property
}