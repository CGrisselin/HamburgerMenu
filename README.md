# HamburgerMenu
Windows 10 styled, customizable Hamburger Menu control for WPF

![Animation](http://imgur.com/x42dTVc "Animation")

## Use
1. Add library reference in xaml:

2. Create a Hamburger Menu control and add menu items:
```
<HamburgerMenu:HamburgerMenu Background="Black" MenuIconColor="CadetBlue" 
                                     SelectionIndicatorColor="CadetBlue" 
                                     MenuItemForeground="#fddd" 
                                     HorizontalAlignment="Left" 
                                     OpenWidth="170" CloseWidth="45"
                                     IconsMargin="10" IconsSize="20"
                                     AnimationDuration="0.2" SlideOpenDelay="0.6">
            <HamburgerMenu:HamburgerMenu.Content>
                <HamburgerMenu:HamburgerMenuItem Icon="Assets/home.png" Text="Home" 
                                                 SelectionCommand="{Binding ElementName=this_}" SelectionCommandParameter="The parameter"
                                                 DockPanel.Dock="Top" />
                <HamburgerMenu:HamburgerMenuItem Icon="Assets/reload.png" Text="Reload" DockPanel.Dock="Bottom" IsSelectable="False"/>
                <HamburgerMenu:HamburgerMenuItem Icon="Assets/search.png" Text="Search" DockPanel.Dock="Bottom"/>
                <HamburgerMenu:HamburgerMenuItem Icon="Assets/favorite.png" Text="Likes" DockPanel.Dock="Top"/>
                <HamburgerMenu:HamburgerMenuItem Icon="Assets/list.png" Text="Lists" DockPanel.Dock="Top"/>
                <HamburgerMenu:HamburgerMenuItem Icon="Assets/person.png" Text="Profile" />
            </HamburgerMenu:HamburgerMenu.Content>
        </HamburgerMenu:HamburgerMenu>
```
