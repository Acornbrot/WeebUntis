using System;
using Avalonia;
using Avalonia.Controls;

namespace WeebUntis.Controls.Panels;

public class AbsoluteVerticalPanel : Panel
{
    // Attached properties for vertical positioning
    public static readonly AttachedProperty<double> TopProperty = AvaloniaProperty.RegisterAttached<
        AbsoluteVerticalPanel,
        Control,
        double
    >("Top", 0.0);

    public static new readonly AttachedProperty<double> HeightProperty =
        AvaloniaProperty.RegisterAttached<AbsoluteVerticalPanel, Control, double>(
            "Height",
            double.NaN
        );

    // Attached properties for horizontal positioning (as percentage, 0.0 to 1.0)
    public static readonly AttachedProperty<double> LeftPercentageProperty =
        AvaloniaProperty.RegisterAttached<AbsoluteVerticalPanel, Control, double>(
            "LeftPercentage",
            0.0
        );

    public static readonly AttachedProperty<double> WidthPercentageProperty =
        AvaloniaProperty.RegisterAttached<AbsoluteVerticalPanel, Control, double>(
            "WidthPercentage",
            1.0
        );

    public static double GetTop(Control element) => element.GetValue(TopProperty);

    public static void SetTop(Control element, double value) =>
        element.SetValue(TopProperty, value);

    public static double GetHeight(Control element) => element.GetValue(HeightProperty);

    public static void SetHeight(Control element, double value) =>
        element.SetValue(HeightProperty, value);

    public static double GetLeftPercentage(Control element) =>
        element.GetValue(LeftPercentageProperty);

    public static void SetLeftPercentage(Control element, double value) =>
        element.SetValue(LeftPercentageProperty, value);

    public static double GetWidthPercentage(Control element) =>
        element.GetValue(WidthPercentageProperty);

    public static void SetWidthPercentage(Control element, double value) =>
        element.SetValue(WidthPercentageProperty, value);

    protected override Size MeasureOverride(Size availableSize)
    {
        double maxHeight = 0;
        double panelWidth = availableSize.Width;

        foreach (Control child in Children)
        {
            var top = GetTop(child);
            var height = GetHeight(child);
            var widthPercentage = GetWidthPercentage(child);

            // Calculate horizontal size
            var childWidth = double.IsInfinity(panelWidth)
                ? double.PositiveInfinity
                : panelWidth * widthPercentage;

            // Calculate vertical size
            var childHeight = double.IsNaN(height) ? double.PositiveInfinity : height;

            child.Measure(new Size(childWidth, childHeight));

            // Determine actual height to use
            var actualHeight = double.IsNaN(height) ? child.DesiredSize.Height : height;

            // Track maximum extent
            maxHeight = Math.Max(maxHeight, top + actualHeight);
        }

        return new Size(panelWidth, maxHeight);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        foreach (Control child in Children)
        {
            var top = GetTop(child);
            var height = GetHeight(child);
            var leftPercentage = GetLeftPercentage(child) / 1000d;
            var widthPercentage = GetWidthPercentage(child) / 1000d;

            // Calculate horizontal positioning
            var left = finalSize.Width * leftPercentage;
            var width = finalSize.Width * widthPercentage;

            // Calculate vertical positioning
            var actualHeight = double.IsNaN(height) ? child.DesiredSize.Height : height;

            child.Arrange(new Rect(left, top, width, actualHeight));
        }

        return finalSize;
    }
}
