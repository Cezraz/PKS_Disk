   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AdminAdmin  5   	       Admin.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Admin     5   	   -   5          � Title$  
MainWindow��5   	   $��408.279��6   $
��1043��6-   ���	#FF494949 � 	Resources 5	         ButtonStyle1 �� 
TargetType �� Property �� Value �� 
TargetType'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 �� TargetProperty
 �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value R� KeyTime R� Value ��Name �� RadiusY �� RadiusX J� RenderTransformOrigin ��Name J� SnapsToDevicePixels �� Triggers P� Property P� Value��%�
  &	        �� 5
   
   " ��6%   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �  5        5   "   CommonStates��. 5   &     
Normal��62     5   &   	MouseOver��. 5   *   �� .��5   .   U� $	 (Shape.StrokeThickness)�6L   $
 	rectangle��6�   .\�5   2     $ 0��6G   $ 7Q�6S   5   /   5   +   5   '     5   &   Pressed��. 5   *   �� .��5   .   U� $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6L   $
 	rectangle��6�   .\�5   2     $ 0��6G   $ 0.785Q�6S   5   /   U� 5   .   $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6L   $
 	rectangle��6�   .\�5   2     $ 0��6G   $ 0.742Q�6S   5    /   U� 5!   .   $U	 O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6L   $
 	rectangle��6�   .\�5"   2     $ 0��6G   $ -0.167Q�6S   5#   /   U� 5$   .   $U	 O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6L   $
 	rectangle��6�   .\�5%   2     $ 0��6G   $ -0.891Q�6S   5&   /   U� 5'   .   $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6L   $
 contentPresenter��6�   .\�5(   2     $ 0��6G   $ 0.788Q�6S   5)   /   U� 5*   .   $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6L   $
 contentPresenter��6�   .\�5+   2     $ 0��6G   $ 0.788Q�6S   5,   /   U� 5-   .   $U	 O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6L   $
 contentPresenter��6�   .\�5.   2     $ 0��6G   $ -0.035Q�6S   5/   /   U� 50   .   $U	 O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6L   $
 contentPresenter��6�   .\�51   2     $ 0��6G   $ -0.189Q�6S   52   /   T� 53   .   $	 (UIElement.OpacityMask)�6L   $
 contentPresenter��6�   ..�54   2   i� $ 0��6I    6U   Z� 55   /   56   +   57   '     58   &   Disabled��62   59   #   .+�5;      �� $ 	rectangle��6(   ���#666565$	 9.5��6J   $	 9.5��6X   
���   �$��4��6u   $ 0.5,0.5&�6�   z�5<   "   V� 5=   &   .~�5>   *   �� �� 5?   *   �� 5@   *   T� 5A   *   5B   '   5D      �� 5E      $ contentPresenter��6/   #��z��6I   ��. # z 6�   #��z��6�   $ 0.5,0.5&�6"  z�5F   "   V� 5G   &   .~�5H   *   �� �� 5I   *   �� 5J   *   T� 5K   *   5L   '   5N      5O       5P      P� 5Q       �@J�	IsFocused . 6;   P� 5R       �@��IsDefaulted . 6=   P� 5S       �@J�IsMouseOver . 6=   P� 5T       � �� . 6;   P� 5U       � {� .  6;   5W      5Y      5Z      5[      .��5\      � .+�5]   
   &   System.Windows.Controls.DataGrid  -   5          Name$ admin��5]   .      AutoGenerateColumns . $
��Left=�6=   $	��350��6X   $��
307,10,0,0q�6e   $	��Top=�6y   $	��718��6�   ���	#FF6E6A6A6�   �� 5^   
   -   5        #���   5^      $	��Add��69   $
��Left=�6G   $��
206,28,0,0q�6b   $	��Top=�6v   $��84��6�   $��25��6�   
�������6�   �� 5_   
   -   5        #���   5_      $��Delete��69   $
��Left=�6J   $��
206,69,0,0q�6e   $	��Top=�6y   $��84��6�   $��24��6�   
�������6�   �� 5`   
   -   5        #���   5`      $��Сhange��69   $
��Left=�6J   $��206,112,0,0q�6e   $	��Top=�6z   $��84��6�   $��24��6�   
����������	#FF6360606�   �� 5a   
   -   5         ��Name$ id_role��5a      $
��Left=�6#   $��25��6>   $��	10,28,0,0q�6J    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $	��136��6�   �� 5b   
   -   5        $ email��5b      $
��Left=�6!   $��23��6<   $��	10,90,0,0q�6H   $
 Wrap=�6[   $	��Top=�6o   $	��136��6�   �� 5c   
   -   5        $ Login��5c      $
��Left=�6!   $��24��6<   $��	9,149,0,0q�6H   $
 Wrap=�6[   $	��Top=�6o   $	��136��6�   �� 5d   
   -	   5        $
 Pass��5d      $
��Left=�6    $��24��6;   $��	9,211,0,0q�6G   $
 Wrap=�6Z   $	��Top=�6n   $	��136��6�   �� 5e   
   -
   5        $
 Name��5e      $
��Left=�6    $��24��6;   $��
10,272,0,0q�6G   $
 Wrap=�6[   $	��Top=�6o   $	��136��6�   �� 5f   
   -   5         ��Name$	 asd��5f   /   $
��Left=�6   $��	57,11,0,0q�6<   $
��Wrap=�6O   $��id role��6c   $	��Top=�6r   $��17��6�   $��39��6�   
�������6�   �� 5g   
   $
��Left=�6   $��	61,68,0,0q�6/   $
��Wrap=�6B   $��Email��6V   $	��Top=�6c   $��16��6{   $��33��6�   
�������6�   �� 5h   
   $
��Left=�6   $��
62,127,0,0q�6/   $
��Wrap=�6C   $��Login��6W   $	��Top=�6d   $��17��6|   $��35��6�   
�������6�   �� 5i   
   $
��Left=�6   $��
52,188,0,0q�6/   $
��Wrap=�6C   $��Password��6W   $	��Top=�6g   $��17��6   $��55��6�   
�������6�   �� 5j   
   $
��Left=�6   $��
62,250,0,0q�6/   $
��Wrap=�6C   $
��Name��6W   $	��Top=�6c   $��17��6{   $��37��6�   
�������6�   �� 5k   
   -   5        $ Phone��5k      $
��Left=�6!   $��25��6<   $��
10,335,0,0q�6H   $
 Wrap=�6\   $	��Top=�6p   $	��136��6�   �� 5l   
   $
��Left=�6   $��
61,314,0,0q�6/   $
��Wrap=�6C   $��Phone��6W   $	��Top=�6d   $��47��6|   $��17��6�   
�������6�   �� 5m   
   -   5         ��Name$ accept��5m   8   #���   6   $��Accept��6I   $
��Left=�6Z   $��206,150,0,0q�6u   $	��Top=�6�   $��84��6�   $��23��6�   
�������6�   5o      5p      