*Files to look at*:
- [Extensions](./CS/AspNetCoreDashboard/wwwroot/Content/Extensions)
- [_Layout.cshtml](./CS/AspNetCoreDashboard/Pages/_Layout.cshtml)
- [Index.cshtml](./CS/AspNetCoreDashboard/Pages/Index.cshtml)

# Dashboard for ASP.NET Core - Custom Properties
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/260212520/)**
<!-- run online end -->


The example shows how to create [custom properties](https://docs.devexpress.com/Dashboard/401702/designer-and-viewer-applications/web-dashboard/client-side-customization/custom-properties?v=20.1) for the Web Dashboard.

## Overview

Custom properties are stored in the **CustomProperties** collection in a structured format. Each custom property in this collection contains the custom property's metadata. 

To apply custom property values to a dashboard, you need to create an extension. The extension is a JavaScript module that you can integrate into your application. Every extension that provides custom property can be divided into the following parts:

1. Model.

    The model is an object that contains the property name, type, and default value. It also specifies on which level the property is created (dashboard, dashboard item or data item container). Use the [Model.registerCustomProperty](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.Model.registerCustomProperty-1?v=20.1) property to register the custom property definition.

2. Viewer

    In this part you modify the viewer part according to the saved custom property value. You can use the client methods and events to change the displayed elements.

3. Designer

    This part contains designer settings. Add editors and control elements to configure and change the custom property's values in the UI. This part is not required if you use the extension in Viewer mode.

4. Event Subscription

    This part contains event subscriptions.
	
## Registration

To register an extension, attach the extension script before the control is rendered and call the `registerExtension` method:

```html
<!DOCTYPE html>

<html>
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>Dashboard Web Application</title>

    <link href="css/site.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function onBeforeRender(control) {
            control.registerExtension(new DevExpress.Dashboard.DashboardPanelExtension(control));
            control.registerExtension(new ChartScaleBreaksExtension(control))
            control.registerExtension(new ChartLineOptionsExtension(control))
            control.registerExtension(new ChartAxisMaxValueExtension(control))
            control.registerExtension(new ChartConstantLinesExtension(control))
            control.registerExtension(new ItemDescriptionExtension(control))
            control.registerExtension(new DashboardDescriptionExtension(control))
        }
    </script>
</head>
<body>
    @RenderBody()

    <script src="js/site.min.js"></script>
    <script src="~/Content/Extensions/ChartConstantLinesExtension.js"></script>
    <script src="~/Content/Extensions/ChartLineOptionsExtension.js"></script>
    <script src="~/Content/Extensions/ChartScaleBreaksExtension.js"></script>
    <script src="~/Content/Extensions/ItemDescriptionExtension.js"></script>
    <script src="~/Content/Extensions/DashboardDescriptionExtension.js"></script>
    <script src="~/Content/Extensions/ChartAxisMaxValueExtension.js"></script>
</body>
</html>
```

## Example structure

The following example contains a set of custom properties that demonstrate different capabilities. Below you find a detailed description for every extension.

### ChartScaleBreaksExtension

[View Extension](./CS/AspNetCoreDashboard/wwwroot/Content/Extensions/ChartScaleBreaksExtension.js)

This extension enables or disables scale breaks for the Chart dashboard item.

![](images/ChartScaleBreaksExtension.png)

**Overview**:
- Adds a custom Boolean property for a specific dashboard item (Chart).
- Integrates a _Scale breaks (Custom)_ section into the _Options_ menu with the [CheckBox](https://js.devexpress.com/DevExtreme/ApiReference/UI_Widgets/dxCheckBox/) widget as an editor.

### ChartLineOptionsExtension

[View Extension](./CS/AspNetCoreDashboard/wwwroot/Content/Extensions/ChartLineOptionsExtension.js)

This extension changes the dash style of each series line in the Chart dashboard item.

![](images/ChartLineOptionsExtension.png)

**Overview**:
- Adds a string custom property for a specific data item container (Chart's series).
- Integrates a _Line Options (Custom)_ section into the data item menu with the [SelectBox](https://js.devexpress.com/DevExtreme/ApiReference/UI_Widgets/dxSelectBox/) widget as an editor.

### DashboardDescriptionExtension

[View Extension](./CS/AspNetCoreDashboard/wwwroot/Content/Extensions/DashboardDescriptionExtension.js)

This extension enables you to set a dashboard's description in the dashboard menu. The dashboard description is displayed when you hover over the info button in the dashboard title. 

![](images/DashboardDescriptionExtension.png)

**Overview**:
- Adds a custom string property for a dashboard.
- Shows how to add a new item to the [ToolBox](https://docs.devexpress.com/Dashboard/117442/designer-and-viewer-applications/web-dashboard/ui-elements/toolbox?v=20.1). In this example, a new item is added to the [dashboard menu](https://docs.devexpress.com/Dashboard/117444/designer-and-viewer-applications/web-dashboard/ui-elements/dashboard-menu?v=20.1).
- Demonstrates how to create complex editors using templates. In this example, it is the [](https://js.devexpress.com/DevExtreme/ApiReference/UI_Widgets/dxPopup/) widgets with the [TextArea](https://js.devexpress.com/DevExtreme/ApiReference/UI_Widgets/dxTextArea/) and [Button](https://js.devexpress.com/DevExtreme/ApiReference/UI_Widgets/dxButtonGroup/) widgets inside.

### ItemDescriptionExtension

[View Extension](./CS/AspNetCoreDashboard/wwwroot/Content/Extensions/ItemDescriptionExtension.js)

This extension enables you to set a description for each dashboard item. The dashboard item description is displayed when you hover over the info button in the item's caption. 

![](images/ItemDescriptionExtension.png)

Overview:
- Adds a custom string property for each dashboard item.
- Integrates a _Description (Custom)_ section into the _Options_ menu with the predefined [buttonGroup](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.Designer.FormItemTemplates?v=20.1#js_devexpress_dashboard_designer_formitemtemplates_buttongroup_static) template.
- Shows how to enable or disable editors depending on a custom property's value. 

### ChartAxisMaxValueExtension

[View Extension](./CS/AspNetCoreDashboard/wwwroot/Content/Extensions/ChartAxisMaxValueExtension.js)

This extension allows you to change the maximum value of the Y-axis in the Chart item. 

![](images/ChartAxisMaxValueExtension.png)

Overview:
- Adds a set of custom properties with [different types](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.Model.CustomPropertyMetadata?v=20.1#js_devexpress_dashboard_model_custompropertymetadata_valuetype) (number, boolean, and string) for a specific dashboard item (Chart).
- Demonstrates how to bind a custom property to a list of data items.
- Shows how to enable or disable editors depending on a custom property's value. 

### ChartConstantLinesExtension

[View Extension](./CS/AspNetCoreDashboard/wwwroot/Content/Extensions/ChartConstantLinesExtension.js)

This extension draws constant lines for the Chart dashboard item.

![](images/ChartConstantLinesExtension.png)

Overview:
- Adds a complex custom property for a specific dashboard item (Chart).
- Shows how to work with complex custom values that are saved as an array.
- Demonstrates how to bind a custom property to a list of data items.
- Customizes export to display the result in the exported document.

## Documentation

- [Client-Side Customization](https://docs.devexpress.com/Dashboard/401701)
- [Custom Properties](https://docs.devexpress.com/Dashboard/401702)

## More Examples

- [ASP.NET Web Forms Dashboard Control - Custom Properties](https://github.com/DevExpress-Examples/asp-net-web-forms-dashboard-custom-properties-sample)
- [ASP.NET MVC Dashboard Control - Custom Properties](https://github.com/DevExpress-Examples/asp-net-mvc-dashboard-custom-properties-sample)
- [Dashboard Component for Angular - Custom Properties](https://github.com/DevExpress-Examples/angular-with-asp-net-core-dashboard-custom-properties-sample)
- [WinForms Dashboard Designer - Custom Properties](https://github.com/DevExpress-Examples/winforms-dashboard-custom-properties)
- [WPF Dashboard Viewer - Custom Properties](https://github.com/DevExpress-Examples/wpf-dashboard-custom-properties)
- [Multiplatform Example - Constant Lines](https://github.com/DevExpress-Examples/dashboard-constant-lines)
