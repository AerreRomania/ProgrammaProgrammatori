   M S B A M L   `   `   `  ����A  =PP.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PP.WPF.BehaviorsPP.WPF.Behaviors  5      5clr-namespace:PP.WPF.ConvertersPP.WPF.Converters  5      +clr-namespace:PP.WPF.ViewsPP.WPF.Views  5      &     PP.WPF.Views.AnalysisArticleView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.2, Version=20.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      5
behavioursclr-namespace:PP.WPF.Behaviors     5      6
convertersclr-namespace:PP.WPF.Converters     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	      f bDevExpress.Xpf.Grid.v20.2.Core, Version=20.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v20.2.Extensions, Version=20.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa	 ]DevExpress.Xpf.Grid.v20.2, Version=20.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   	 5
      ]
 YDevExpress.Mvvm.v20.2, Version=20.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 
  5      g cDevExpress.Xpf.Scheduling.v20.2, Version=20.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aOdxschtBhttp://schemas.devexpress.com/winfx/2008/xaml/scheduling/themekeys  5      Ldxschv?http://schemas.devexpress.com/winfx/2008/xaml/scheduling/visual  5      &localclr-namespace:PP.WPF.Views  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        � 	Resources  5       
  BaseCB �� 
TargetType �� Property �� Value  BaseWrap 
 BaseDP(  "System.Windows.Controls.DatePicker  
RightLabel  	LeftLabel  ColumnHeaderStyle  BaseHeaderStyle(  "DevExpress.Xpf.Grid.BaseGridHeader.  (DevExpress.Xpf.Grid.BaseGridColumnHeader  BaseGrid1   +PP.WPF.Converters.BooleanToIntegerConverter  BoolToIntConverter��%�  &	        &	 �     &	     &	 �    &	 �    &	 H    &	 �    &	 C    &	 �    �� 5   
   " ��6   .�5      ��  � ��$
 Left=�64   �� 5       � ��$ Center=�62   �� 5       � ��$	 120��6&   �� 5       � ��$ 10,0,0,0q�6'   5      �� 5   
   " �6!   .�5      ��  � ��$ 10,0,0,0q�6'   5      �� 5   
   "  6   .�5      ��  � ��$ 2q�6'   �� 5       � ��$
 Left=�64   �� 5        � ��$ Center=�62   �� 5!       � ��$ 0q�60   5"      �� 5#   
   " ��6#   .�5$      ��  � ��$ Right=�64   �� 5%       � ��$ Center=�62   �� 5&       � ��$ 14(�6)   �� 5'       � ��$ DemiBold"�6+   5(      �� 5)   
   " ��6"   .�5*      ��  � ��$
 Left=�64   �� 5+       � ��$ Center=�62   �� 5,       � ��$ 14(�6)   �� 5-       � ��$ DemiBold"�6+   5.      �� 5/   
   " ��6*   .�50      ��  � ��$ 14(�6)   �� 51       � ��
 �����6+   52      �� 54   
   "  6(   .�55      ��  �@ ShowFilterButtonOnHover$ False��6Q   �� 56       � �� �#3232326+   57      �� 58   
   " �6!   .�59      ��  � �� �#FFC0006+   5:        5<   
   6/   5=      .��5?      �  � ColumnDefinitions 5@   
   �� 5A      $	��300 �6   �� 5B      $	��300 �6   �� 5C      $	��300 �6   �� 5D      $��* �6    � RowDefinitions 5F   
   �� 5G      $��60 �6   �� 5H      $��* �6   .+�5K   
   � #��[ 6    5L      �� 5M      $��* �6    �� 5N      $��* �6    .+�5Q      � $��0��6   #��[ 6%   .+�5R      �� $��Data Inizio: ��6   #��[ 60     5S         SelectedDateFormat$ Short=�5U      #��[ 5V         SelectedDate 5T      �� *	StartDate+ �� Mode$ TwoWay=�5V      5W      � 5X      $��1��6   #��[ 6%   .+�5Y      �� $��Data Fine: ��6   #��[ 6.     5Z      $��10,0,0,0q�5[      $ Short=�5]      #��[ 5^       5\      �� *	EndDate+$ TwoWay=�5^      5_      5`      � 5b   
   $��1��6   #��[ 6    5c      �� 5d      $��* �6    �� 5e      $��* �6    .+�5h      � $��0��6   #��[ 6%   .+�5i      �� $��	Cliente: ��6   #��[ 6,   �� 5j      -   5        	 ��Name$	 cbClient��5k      $��17,0,0,0q�5l      
 �� DisplayMemberPath$
 Client��5m       �� 
IsEditable$
 True��5n      #��[  5q      ��5o      �� *ClientsList+ �� SelectedValue 5p      �� *SelectedClient+$ TwoWay=�5q      5r      � 5s      $��1��6   #��[ 6%   .+�5t      �� $��
Articolo: ��6   #��[ 6-   �� 5u      -   5        $	 	ArticleCB��5v      $
 Articol��5w      $
 True��5y      #��[  5}      ��5z      �� *ArticleList+ �� SelectedIndex 5{      �� *SelectedIndex+$ TwoWay=� 5|      �� *SelectedArticle+$ TwoWay=�5}      5~      5      � 5�   
   $��2��6   #��[ 6    5�      �� 5�      $��* �6    �� 5�      $��* �6    .+�5�      � #��[ 6   .+�5�      �� $��Current year: ��6   #��[ 61   �� 5�      $��10,4,0,0q�5�      $��Center=�5�       d� 	IsChecked 5�      �� *	IsChecked+$ TwoWay=�5�      � 5�      $��1��6   #��[ 6%   .+�5�      �� $��	Finezza: ��6   #��[ 6,   �� 5�      #��[ 6C   ��6   �� *SelectedArticle.Finete+6C   5�      5�      � 5�   
   $��3��6   #��[ 6    5�      �� 5�      $	��100 �6#   �� 5�      $	��100 �6#   5�      % 	 DevExpress.Xpf.Grid.GridControl  5�   
   -   5          Name$ AnalysisReport��5�      $��1��5�      $��4��5�      $��0q�5�         Padding$ 0q�5�      )  #DevExpress.Xpf.Grid.DataControlBase   SelectionMode$	 Row=�5�         
ShowBorder$ False��5�         ItemsSource 5�      �� *AnalysisList+$ TwoWay=�2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5�      &    PP.WPF.Behaviors.BestFitBehavior  5�         View 5�      #	 	 DevExpress.Xpf.Grid.TableView	  5�       	  AllowBestFit$
 True��5�      &
   DevExpress.Xpf.Grid.DataViewBase 
  AllowColumnFiltering$
 True��5�       
  AllowColumnMoving$ False��5�      & 	  DevExpress.Xpf.Grid.GridViewBase   AllowGrouping$ False��5�       	  AllowResizing$ False��5�       	  AlternateRowBackground
 ����� 	  	AutoWidth$
 True��5�      $��14(�5�       	  ShowAutoFilterRow$ False��5�       	  ShowBandsPanel$ False��5�         ShowGroupPanel$ False��5�        	  ShowIndicator$  False��5�      ! 
  ShowSearchPanelMode$! Never=�5�      "" 	  VerticalScrollbarVisibility$
" Auto=�5�      #   Columns# 5�      $ 	 DevExpress.Xpf.Grid.GridColumn  5�      $  DevExpress.Xpf.Grid.ColumnBase$   ColumnHeaderContentStyle#$ [ 5�      %   EditFormVisible	% False5�      &   	FieldName$& 
Programmer��5�      $  DevExpress.Xpf.Grid.BaseColumn'   Fixed$
' Left=�5�      (   Header)  #DevExpress.Xpf.Core.ObjectConverter$( 	Operatore 5�      )   HeaderStyle#) [ 5�      '*    HorizontalHeaderContentAlignment$* Center=�5�      +   ReadOnly$
+ True��5�      ,   EditSettings, 5�      6  0DevExpress.Xpf.Editors.Settings.TextEditSettings  5�      6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!-   HorizontalContentAlignment$- Center=�6/   5�        5�      #$ [ 5�      	% False5�      $& JobTypeName��5�      $
' Left=�5�      $( Dettaglio Attivita 5�      #) [ 5�      $* Center=�5�      $
+ True��5�      , 5�        5�      $- Center=�6/   5�        5�      #$ [ 5�      	% False5�      $& ComputerHours��5�      $
' Left=�5�      $( Computer 5�      #) [ 5�      $* Center=�5�      $
+ True��5�      , 5�        5�      $- Center=�6/   .   DisplayFormat$. N2��6S   5�        5�      #$ [ 5�      $& ComputerMachineHours��5�      $
' Left=�5�      $( ComputerMacchina 5�      #) [ 5�      $* Center=�5�      $
+ True��5�      , 5�        5�      $- Center=�6/   $. N2��6S   5�        5�      #$ [ 5�      $& MachineHours��5�      $
' Left=�5�      $( Macchina 5�      #) [ 5�      $* Center=�5�      $
+ True��5�      , 5�        5�      $- Center=�6/   $. N2��6S   5�        5�      #$ [ 5�      $& Total��5      $
' Left=�5     $( Totale 5     #) [ 5     $* Center=�5     $
+ True��5     , 5       5     $- Center=�6/   $. N2��6S   5	     5     5     5     