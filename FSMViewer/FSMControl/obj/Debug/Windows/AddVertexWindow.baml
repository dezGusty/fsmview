   M S B A M L   `   `   `  ����  
FSMControl(    "FSMControl.Windows.AddVertexWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  AddVertexWindow��5   	   ���#3D5C5C$	��300��5      $	��350��6,    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$ 
ToolWindow=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5	      �  � ColumnDefinitions 5
      �� 5      $��1093* �6   �� 5      $
��250* �6    � RowDefinitions 5      �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $	��100 �6   .+�5      �� -   5        $��Add it to the Graph��5      $
��Left=�6+   $��
85,47,0,10q�6F   $��2��6Z   $	��193��6g   6s   �� 5      $��Name of the Vertex: ��6   
�������$
��Left=�6>   $��	15,53,0,0q�6Y   $��1��6l   $	��Top=�6y   $	��115��6�   $��35��6�   �� 5      -   5        $��txtAddVertex��5      $��130,53,41,89q�6"   $��1��68   -  'System.Windows.Input.KeyboardNavigation   ControlTabNavigation$
 Once=�6E   $��2��6t   �� 5      $��Reentry Using 
Trigger:��6   
�������$
��Left=�6E   $��
15,107,0,0q�6`   $��1��6t   $	��Top=�6�   $	��115��6�   $��50��6�   �� 5      -   5         ��Name$ txt_Reentry��5      $��130,110,41,31q�6#   $��1��6:   $
 Once=�6G   $��2��6v   �� 5      -   5        $ txt_DefaultHandler��5      $��130,167,41,75q�6*   $��1��6A   $
 Once=�6N   $��2��6}   $��2��6�   �� 5      $��Default Handler:��6   
�������$
��Left=�6:   $��
15,167,0,0q�6U   $��1��6i   $	��Top=�6v   $	��115��6�   $��50��6�   $��2��6�   5      5      