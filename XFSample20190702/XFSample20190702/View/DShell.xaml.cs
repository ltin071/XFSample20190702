using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFSample20190702.Model;

namespace XFSample20190702.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DShell : Shell
	{
        public enum DisplayType
        {
            BOTH,
            FLYOUT,
            TABBAR
        }

        private List<DPageDefinition> pageDefinitions = new List<DPageDefinition>();
        
        public DShell(List<DPageDefinition> pageDefinitions, DisplayType displayType)
        {
            InitializeComponent();
            
            SetBackgroundColor(this, Color.Black);
            SetForegroundColor(this, Color.White);
            SetTitleColor(this, Color.White);
            SetUnselectedColor(this, Color.Gray);
            SetDisabledColor(this, Color.Gray);
            SetTabBarBackgroundColor(this, Color.White);
            SetTabBarForegroundColor(this, Color.Black);
            SetTabBarTitleColor(this, Color.Black);
            SetTabBarUnselectedColor(this, Color.Gray);
            SetTabBarDisabledColor(this, Color.Gray);

            GeneratePages(pageDefinitions, displayType);
            
        }

        //
        // DON'T change this function unless you know what you are doing
        // WHAT: Read DPageDefinition and generate pages and insert to shell.item
        // WHY: To centralize page generation in shell implementation
        //
        private void GeneratePages(List<DPageDefinition> pageDefinitions, DisplayType displayType)
        {
            if (pageDefinitions == null)
            {
                Console.WriteLine("Error! Page definitions is null! Please insert it into DShell.");
                return;
            }
            switch (displayType)
            {
                case DisplayType.BOTH:
                    //Flyout + tabbar
                    //Hierarchy: One Flyout > Multiple Tab / ShellContent
                    FlyoutItem flyoutItem = new FlyoutItem()
                    {
                        FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
                    };
                    foreach (DPageDefinition pageDefinition in pageDefinitions)
                    {
                        flyoutItem.Items.Add(GetShellSectionFromPageDefinition(pageDefinition));
                    }
                    Items.Add(flyoutItem);
                    break;
                case DisplayType.FLYOUT:
                    //Flyout only
                    //Hierarchy: Multiple Flyout > One ShellContent
                    foreach (DPageDefinition pageDefinition in pageDefinitions)
                    {
                        flyoutItem = new FlyoutItem()
                        {
                            Title = pageDefinition.title,
                            Icon = ImageSource.FromFile(pageDefinition.icon),
                        };
                        flyoutItem.Items.Add(GetShellSectionFromPageDefinition(pageDefinition));
                        Items.Add(flyoutItem);
                    }
                    break;
                case DisplayType.TABBAR:
                    //Tabbar only
                    //Hierarchy: Tabber > Tab / ShellContent
                    TabBar tabbarItem = new TabBar();

                    foreach (DPageDefinition pageDefinition in pageDefinitions)
                    {
                        tabbarItem.Items.Add(GetShellSectionFromPageDefinition(pageDefinition));
                    }
                    Items.Add(tabbarItem);
                    break;
            }
        }

        private ShellSection GetShellSectionFromPageDefinition(DPageDefinition pageDefinition)
        {
            if (pageDefinition.subPages != null)
            {
                Tab tab = new Tab()
                {
                    Title = pageDefinition.title,
                    Icon = ImageSource.FromFile(pageDefinition.icon),
                };
                foreach (DPageDefinition subPageDefinition in pageDefinition.subPages)
                {
                    ShellContent shellContent = new ShellContent()
                    {
                        Title = subPageDefinition.title,
                        ContentTemplate = new DataTemplate(subPageDefinition.pageClass)
                    };
                    tab.Items.Add(shellContent);
                }
                return tab;
            }
            else
            {
                ShellContent shellContent = new ShellContent()
                {
                    Title = pageDefinition.title,
                    Icon = ImageSource.FromFile(pageDefinition.icon),
                    ContentTemplate = new DataTemplate(pageDefinition.pageClass)
                };
                return shellContent;
            }
        }
    }
}