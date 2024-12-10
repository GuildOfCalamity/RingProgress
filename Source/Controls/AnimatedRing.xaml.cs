using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace RingProgressDemo.Controls;

public partial class AnimatedRing : UserControl
{
    bool _hasAppliedTemplate = false;

    #region [Dependency Properties]
    /// <summary>
    ///   Define our arc height property
    /// </summary>
    public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
        nameof(Height),
        typeof(double),
        typeof(AnimatedRing),
        new PropertyMetadata(50d, OnHeightChanged));
    public double Height
    {
        get { return (double)this.GetValue(HeightProperty); }
        set { this.SetValue(HeightProperty, value); }
    }
    static void OnHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var cp = d as AnimatedRing;
        if (cp != null && e.NewValue != null)
        {
            Debug.WriteLine($"[INFO] Height changed to {e.NewValue}");
        }
    }

    /// <summary>
    ///   Define our arc width property
    /// </summary>
    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(AnimatedRing),
        new PropertyMetadata(50d, OnWidthChanged));
    public double Width
    {
        get { return (double)this.GetValue(WidthProperty); }
        set { this.SetValue(WidthProperty, value); }
    }
    static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var cp = d as AnimatedRing;
        if (cp != null && e.NewValue != null)
        {
            Debug.WriteLine($"[INFO] Width changed to {e.NewValue}");
        }
    }
    #endregion

    public AnimatedRing()
    {
        InitializeComponent();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _hasAppliedTemplate = true;
        //leadingArc.GradientStops.Clear();
        //leadingArc.GradientStops.Add(new GradientStop(Color.FromArgb(255, 204, 255, 0), 0.2));
        //leadingArc.GradientStops.Add(new GradientStop(Color.FromArgb(51, 255, 204, 0), 1.0));
        Debug.WriteLine($"[INFO] {nameof(AnimatedRing)} template has been applied.");
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        Debug.WriteLine($"[INFO] {nameof(AnimatedRing)} is measured to be {availableSize}");
        return base.MeasureOverride(availableSize);
    }
}
