   M S B A M L   `   `   `  ����G  CMaterialMenu, Version=0.0.4.5, Culture=neutral, PublicKeyToken=null+clr-namespace:MaterialMenuMaterialMenu  5          MaterialMenu.SideMenu   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:MaterialMenu     5      -   5        $��Control��5	        � 	Resources  5
         ButtonBackground �� Color �� ElementName  ButtonHover 
 Shadow   MaterialMenu.MenuButton �� 
TargetType �� Property �� Value��%q  &	        &	 ?     &	 �     '  �     �� 5   
    63   �� *ButtonHover+$ Control��6h   �� 5   
    6.   �� *ButtonBackground+$ Control��6h   �� 5   
    6)   �� *ShadowBackground+$ Control��6c   �� 5   
   "  6   .�5      ��  � ��# �   6+   6X   �� 5       �@ HoverBackground# �  60   6X   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      -   5         ��Name$ 
MenuColumn��5      $	��400 �63   6A   �� 5      -   5        $ ShadowColumn��5      $��0 �65   6A   .+�5   
   �� -   5         ��Name$ MenuPresenter��5      $��0��62   ��6B   �� *Menu+$ Control��6r   �� 5   
   -   5        $��1��5      #���  6!   6e   5      5      