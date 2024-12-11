using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RingProgressDemo.Controls;

public partial class AnimatedRing : UserControl
{
    bool _hasAppliedTemplate = false;
    Storyboard _storyboard = new Storyboard();

    #region [Dependency Properties]
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
        var ar = d as AnimatedRing;
        if (ar != null && e.NewValue != null)
        {
            Debug.WriteLine($"[INFO] Height changed to {e.NewValue}");
        }
    }

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
        var ar = d as AnimatedRing;
        if (ar != null && e.NewValue != null)
        {
            Debug.WriteLine($"[INFO] Width changed to {e.NewValue}");
        }
    }

    public static readonly DependencyProperty RingColorProperty = DependencyProperty.Register(
        nameof(RingColor), 
        typeof(Color), 
        typeof(AnimatedRing),
        new PropertyMetadata(Colors.SpringGreen, OnRingColorChanged));
    public Color RingColor
    {
        get { return (Color)this.GetValue(RingColorProperty); }
        set { this.SetValue(RingColorProperty, value); }
    }
    static void OnRingColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ar = d as AnimatedRing;
        if (ar != null && e.NewValue != null)
        {
            var clr = (Color)e.NewValue;
            ar.leadingArc.GradientStops.Clear();
            ar.leadingArc.GradientStops.Add(new GradientStop(Color.FromArgb(255, clr.R, clr.G, clr.B), 0.2));
            ar.leadingArc.GradientStops.Add(new GradientStop(Color.FromArgb(51, clr.R, clr.G, clr.B), 1.0));
            ar.trailingArc.GradientStops.Clear();
            ar.trailingArc.GradientStops.Add(new GradientStop(Color.FromArgb(00, clr.R, clr.G, clr.B), 0.1));
            ar.trailingArc.GradientStops.Add(new GradientStop(Color.FromArgb(63, clr.R, clr.G, clr.B), 1.0));
            ar.pointLeader.Fill = new SolidColorBrush(clr);
            Debug.WriteLine($"[INFO] Ring color changed to {clr}");
        }
    }

    public static readonly DependencyProperty TrackColorProperty = DependencyProperty.Register(
        nameof(TrackColor),
        typeof(Color),
        typeof(AnimatedRing),
        new PropertyMetadata(Colors.Black, OnTrackColorChanged));
    public Color TrackColor
    {
        get { return (Color)this.GetValue(TrackColorProperty); }
        set { this.SetValue(TrackColorProperty, value); }
    }
    static void OnTrackColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ar = d as AnimatedRing;
        if (ar != null && e.NewValue != null)
        {
            var clr = (Color)e.NewValue;
            ar.track.Stroke = new SolidColorBrush(clr);
            Debug.WriteLine($"[INFO] Track color changed to {clr}");
        }
    }

    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
        nameof(IsActive),
        typeof(bool),
        typeof(AnimatedRing),
        new PropertyMetadata(false, OnIsActiveChanged));
    public bool IsActive
    {
        get { return (bool)this.GetValue(IsActiveProperty); }
        set { this.SetValue(IsActiveProperty, value); }
    }
    static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ar = d as AnimatedRing;
        if (ar != null && e.NewValue != null)
        {
            var active = (bool)e.NewValue;
            Debug.WriteLine($"[INFO] Active changed to {active}");
            if (active)
                ar.StartRotateAngleAnimation();
            else
                ar.StopRotateAngleAnimation();
        }
    }

    public static readonly DependencyProperty EasingProperty = DependencyProperty.Register(
        nameof(Easing),
        typeof(IEasingFunction),
        typeof(AnimatedRing),
        new PropertyMetadata(new CubicEase() { EasingMode = EasingMode.EaseInOut }, OnEasingChanged));
    public IEasingFunction Easing
    {
        get { return (IEasingFunction)this.GetValue(EasingProperty); }
        set { this.SetValue(EasingProperty, value); }
    }
    static void OnEasingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ar = d as AnimatedRing;
        if (ar != null)
        {
            var easing = (IEasingFunction)e.NewValue;
            ar.ConfigureAnimation(easing, ar.Time);
            // If the easing function is changed the storyboard must 
            // be stopped and restarted for the changes to take effect.
            if (ar.IsActive)
            {
                ar._storyboard?.Stop();
                ar._storyboard?.Begin();
            }
        }
    }

    public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
        nameof(Time),
        typeof(TimeSpan),
        typeof(AnimatedRing),
        new PropertyMetadata(TimeSpan.FromSeconds(2.0), OnTimeChanged));
    public TimeSpan Time
    {
        get { return (TimeSpan)this.GetValue(TimeProperty); }
        set { this.SetValue(TimeProperty, value); }
    }
    static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ar = d as AnimatedRing;
        if (ar != null && e.NewValue != null)
        {
            var time = (TimeSpan)e.NewValue;
            ar.ConfigureAnimation(ar.Easing, time);
            // If the timespan is changed the storyboard must be
            // stopped and restarted for the changes to take effect.
            if (ar.IsActive)
            {
                ar._storyboard?.Stop();
                ar._storyboard?.Begin();
            }
        }
    }
    #endregion

    public AnimatedRing()
    {
        this.InitializeComponent();
        ConfigureAnimation(Easing, TimeSpan.FromSeconds(2.0));
        this.Unloaded += AnimatedRingOnUnloaded;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _hasAppliedTemplate = true;
        Debug.WriteLine($"[INFO] {nameof(AnimatedRing)} template has been applied.");
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        Debug.WriteLine($"[INFO] {nameof(AnimatedRing)} is measured to be {availableSize}");
        
        if (IsActive)
            container.Visibility = Visibility.Visible;
        else
            container.Visibility = Visibility.Collapsed;

        return base.MeasureOverride(availableSize);
    }

    /// <summary>
    /// This assumes that the Grid has a RenderTransform that includes 
    /// a TransformGroup with a RotateTransform at the specified index.
    /// </summary>
    /// <param name="easing"><see cref="IEasingFunction"/>, or null for no easing</param>
    /// <param name="time"><see cref="TimeSpan"/></param>
    public void ConfigureAnimation(IEasingFunction easing, TimeSpan time)
    {
        _storyboard.Children.Clear();
        DoubleAnimation animation = new DoubleAnimation
        {
            Duration = new Duration(time),
            RepeatBehavior = RepeatBehavior.Forever,
            To = 360,
            EasingFunction = easing, // EasingFunction = (EasingFunctionBase)Application.Current.Resources["CubicEase"]
        };
        _storyboard.Children.Add(animation);
        Storyboard.SetTarget(animation, Circle);
        Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
    }

    public void StartRotateAngleAnimation()
    {
        container.Visibility = Visibility.Visible;
        _storyboard?.Begin();
    }

    public void StopRotateAngleAnimation()
    {
        container.Visibility = Visibility.Collapsed;
        _storyboard?.Stop();
    }

    /// <summary>
    /// Make sure that the animation isn't running if the control becomes unloaded.
    /// </summary>
    void AnimatedRingOnUnloaded(object sender, RoutedEventArgs e) => _storyboard?.Stop();
}
