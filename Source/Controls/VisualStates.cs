﻿using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace RingProgressDemo.Controls;

/// <summary>
///     Names and helpers for visual states in the controls.
/// </summary>
internal static class VisualStates
{
    /// <summary>
    ///     Gets the implementation root of the Control.
    /// </summary>
    /// <param name="dependencyObject">The DependencyObject.</param>
    /// <remarks>
    ///     Implements Silverlight's corresponding internal property on Control.
    /// </remarks>
    /// <returns>Returns the implementation root or null.</returns>
    public static FrameworkElement? GetImplementationRoot(DependencyObject dependencyObject)
    {
        Debug.Assert(dependencyObject != null, "DependencyObject should not be null.");
        return 1 == VisualTreeHelper.GetChildrenCount(dependencyObject) ? VisualTreeHelper.GetChild(dependencyObject, 0) as FrameworkElement : null;
    }

    /// <summary>
    ///     Use VisualStateManager to change the visual state of the control.
    /// </summary>
    /// <param name="control">
    ///     Control whose visual state is being changed.
    /// </param>
    /// <param name="useTransitions">
    ///     A value indicating whether to use transitions when updating the
    ///     visual state, or to snap directly to the new visual state.
    /// </param>
    /// <param name="stateNames">
    ///     Ordered list of state names and fallback states to transition into.
    ///     Only the first state to be found will be used.
    /// </param>
    public static void GoToState(Control control, bool useTransitions, params string[] stateNames)
    {
        Debug.Assert(control != null, $"{nameof(control)} should not be null");
        Debug.Assert(stateNames != null, $"{nameof(stateNames)} should not be null");
        Debug.Assert(stateNames.Length > 0, $"{nameof(stateNames)} should not be empty");
        foreach (var name in stateNames)
        {
            if (VisualStateManager.GoToState(control, name, useTransitions))
                break;
        }
    }

    /// <summary>
    ///     This method tries to get the named VisualStateGroup for the
    ///     dependency object. The provided object's ImplementationRoot will be
    ///     looked up in this call.
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <param name="groupName">The visual state group's name.</param>
    /// <returns>Returns null or the VisualStateGroup object.</returns>
    public static VisualStateGroup? TryGetVisualStateGroup(DependencyObject dependencyObject, string groupName)
    {
        var root = GetImplementationRoot(dependencyObject);
        if (root == null) { return null; }
        return VisualStateManager.GetVisualStateGroups(root).OfType<VisualStateGroup>().Where(group => string.CompareOrdinal(groupName, group.Name) == 0).FirstOrDefault();
    }

    #region GroupCommon

    /// <summary>
    ///     Common state group.
    /// </summary>
    public const string GroupCommon = "CommonStates";

    /// <summary>
    ///     Disabled state of the Common state group.
    /// </summary>
    public const string StateDisabled = "Disabled";

    /// <summary>
    ///     MouseOver state of the Common state group.
    /// </summary>
    public const string StateMouseOver = "MouseOver";

    /// <summary>
    ///     Normal state of the Common state group.
    /// </summary>
    public const string StateNormal = "Normal";

    /// <summary>
    ///     Pressed state of the Common state group.
    /// </summary>
    public const string StatePressed = "Pressed";

    /// <summary>
    ///     Normal state of the Common state group.
    /// </summary>
    public const string StateReadOnly = "ReadOnly";

    #endregion GroupCommon

    #region GroupFocus

    /// <summary>
    ///     Focus state group.
    /// </summary>
    public const string GroupFocus = "FocusStates";

    /// <summary>
    ///     Focused state of the Focus state group.
    /// </summary>
    public const string StateFocused = "Focused";

    /// <summary>
    ///     Unfocused state of the Focus state group.
    /// </summary>
    public const string StateUnfocused = "Unfocused";

    #endregion GroupFocus

    #region GroupSelection

    /// <summary>
    ///     Selection state group.
    /// </summary>
    public const string GroupSelection = "SelectionStates";

    /// <summary>
    ///     Selected state of the Selection state group.
    /// </summary>
    public const string StateSelected = "Selected";

    /// <summary>
    ///     Selected inactive state of the Selection state group.
    /// </summary>
    public const string StateSelectedInactive = "SelectedInactive";

    /// <summary>
    ///     Unselected state of the Selection state group.
    /// </summary>
    public const string StateUnselected = "Unselected";

    #endregion GroupSelection

    #region GroupExpansion

    /// <summary>
    ///     Expansion state group.
    /// </summary>
    public const string GroupExpansion = "ExpansionStates";

    /// <summary>
    ///     Collapsed state of the Expansion state group.
    /// </summary>
    public const string StateCollapsed = "Collapsed";

    /// <summary>
    ///     Expanded state of the Expansion state group.
    /// </summary>
    public const string StateExpanded = "Expanded";

    #endregion GroupExpansion

    #region GroupPopup

    /// <summary>
    ///     Popup state group.
    /// </summary>
    public const string GroupPopup = "PopupStates";

    /// <summary>
    ///     Closed state of the Popup state group.
    /// </summary>
    public const string StatePopupClosed = "PopupClosed";

    /// <summary>
    ///     Opened state of the Popup state group.
    /// </summary>
    public const string StatePopupOpened = "PopupOpened";

    #endregion GroupPopup

    #region GroupValidation

    /// <summary>
    ///     ValidationStates state group.
    /// </summary>
    public const string GroupValidation = "ValidationStates";

    /// <summary>
    ///     Invalid, focused state for the ValidationStates group.
    /// </summary>
    public const string StateInvalidFocused = "InvalidFocused";

    /// <summary>
    ///     Invalid, unfocused state for the ValidationStates group.
    /// </summary>
    public const string StateInvalidUnfocused = "InvalidUnfocused";

    /// <summary>
    ///     The valid state for the ValidationStates group.
    /// </summary>
    public const string StateValid = "Valid";

    #endregion GroupValidation

    #region GroupExpandDirection

    /// <summary>
    ///     ExpandDirection state group.
    /// </summary>
    public const string GroupExpandDirection = "ExpandDirectionStates";

    /// <summary>
    ///     Down expand direction state of ExpandDirection state group.
    /// </summary>
    public const string StateExpandDown = "ExpandDown";

    /// <summary>
    ///     Left expand direction state of ExpandDirection state group.
    /// </summary>
    public const string StateExpandLeft = "ExpandLeft";

    /// <summary>
    ///     Right expand direction state of ExpandDirection state group.
    /// </summary>
    public const string StateExpandRight = "ExpandRight";

    /// <summary>
    ///     Up expand direction state of ExpandDirection state group.
    /// </summary>
    public const string StateExpandUp = "ExpandUp";

    #endregion GroupExpandDirection

    #region GroupHasItems

    /// <summary>
    ///     HasItems state group.
    /// </summary>
    public const string GroupHasItems = "HasItemsStates";

    /// <summary>
    ///     HasItems state of the HasItems state group.
    /// </summary>
    public const string StateHasItems = "HasItems";

    /// <summary>
    ///     NoItems state of the HasItems state group.
    /// </summary>
    public const string StateNoItems = "NoItems";

    #endregion GroupHasItems

    #region GroupIncrease

    /// <summary>
    ///     Increment state group.
    /// </summary>
    public const string GroupIncrease = "IncreaseStates";

    /// <summary>
    ///     State disabled for increment group.
    /// </summary>
    public const string StateIncreaseDisabled = "IncreaseDisabled";

    /// <summary>
    ///     State enabled for increment group.
    /// </summary>
    public const string StateIncreaseEnabled = "IncreaseEnabled";

    #endregion GroupIncrease

    #region GroupDecrease

    /// <summary>
    ///     Decrement state group.
    /// </summary>
    public const string GroupDecrease = "DecreaseStates";

    /// <summary>
    ///     State disabled for decrement group.
    /// </summary>
    public const string StateDecreaseDisabled = "DecreaseDisabled";

    /// <summary>
    ///     State enabled for decrement group.
    /// </summary>
    public const string StateDecreaseEnabled = "DecreaseEnabled";

    #endregion GroupDecrease

    #region GroupIteractionMode

    /// <summary>
    ///     InteractionMode state group.
    /// </summary>
    public const string GroupInteractionMode = "InteractionModeStates";

    /// <summary>
    ///     Display of the DisplayMode state group.
    /// </summary>
    public const string StateDisplay = "Display";

    /// <summary>
    ///     Edit of the DisplayMode state group.
    /// </summary>
    public const string StateEdit = "Edit";

    #endregion GroupIteractionMode

    #region GroupLocked

    /// <summary>
    ///     DisplayMode state group.
    /// </summary>
    public const string GroupLocked = "LockedStates";

    /// <summary>
    ///     Edit of the DisplayMode state group.
    /// </summary>
    public const string StateLocked = "Locked";

    /// <summary>
    ///     Display of the DisplayMode state group.
    /// </summary>
    public const string StateUnlocked = "Unlocked";

    #endregion GroupLocked

    #region GroupActive

    /// <summary>
    ///     Active state group.
    /// </summary>
    public const string GroupActive = "ActiveStates";

    /// <summary>
    ///     Active state.
    /// </summary>
    public const string StateActive = "Active";

    /// <summary>
    ///     Inactive state.
    /// </summary>
    public const string StateInactive = "Inactive";

    #endregion GroupActive

    #region GroupWatermark

    /// <summary>
    ///     Watermark state group.
    /// </summary>
    public const string GroupWatermark = "WatermarkStates";

    /// <summary>
    ///     Non-watermarked state.
    /// </summary>
    public const string StateUnwatermarked = "Unwatermarked";

    /// <summary>
    ///     Watermarked state.
    /// </summary>
    public const string StateWatermarked = "Watermarked";

    #endregion GroupWatermark

    #region GroupCalendarButtonFocus

    /// <summary>
    ///     CalendarButtons Focus state group.
    /// </summary>
    public const string GroupCalendarButtonFocus = "CalendarButtonFocusStates";

    /// <summary>
    ///     Focused state for Calendar Buttons.
    /// </summary>
    public const string StateCalendarButtonFocused = "CalendarButtonFocused";

    /// <summary>
    ///     Unfocused state for Calendar Buttons.
    /// </summary>
    public const string StateCalendarButtonUnfocused = "CalendarButtonUnfocused";

    #endregion GroupCalendarButtonFocus

    #region GroupBusyStatus

    /// <summary>
    ///     Busyness group name.
    /// </summary>
    public const string GroupBusyStatus = "BusyStatusStates";

    /// <summary>
    ///     Busy state for BusyIndicator.
    /// </summary>
    public const string StateBusy = "Busy";

    /// <summary>
    ///     Idle state for BusyIndicator.
    /// </summary>
    public const string StateIdle = "Idle";

    #endregion GroupBusyStatus

    #region GroupVisibility

    /// <summary>
    ///     BusyDisplay group.
    /// </summary>
    public const string GroupVisibility = "VisibilityStates";

    /// <summary>
    ///     Hidden state name for BusyIndicator.
    /// </summary>
    public const string StateHidden = "Hidden";

    /// <summary>
    ///     Visible state name for BusyIndicator.
    /// </summary>
    public const string StateVisible = "Visible";

    #endregion GroupVisibility
}
