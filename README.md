# MVC Master Layout Share #
## Package information ##
This package allows you to share MVC Views between different projects / applications that live on the same server. Typically, this might be used on an Intranet where many small applications all live within the same all encompassing look-and-feel. This allows you to create one default Layout.cshtml file (or multiple) that you can call from your client applications. By extension, it also allows you to share other content, such as images, scripts & stylesheets (but hey, you could just link to them before anyway...)

## Source Code ##
For more information on the project, to see any outstanding bugs and feature implementations you can fork us on GitHub at: 
https://github.com/Amadiere/MVCMasterLayoutShare

## Example ##
Included are two projects:
MVCMasterLayoutShare.Web.Primary : This is a default "Intranet" MVC4 project with minor modifications and an additional theme.
MVCMasterLayoutShare.Web.Secondary: This is a default "Empty" MVC4 project with a simple controller & index method which use the theme from the primary project.