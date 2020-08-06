using Godot;
using System;

public class Button_Count_Increase : Button
{
	private int _counter = 0;
	private Label _label;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_label = GetNode<Label>("../Label_Counter");
	}

	private void _on_Button_Count_Increase_pressed()
	{
		_counter++;
		_label.Text = _counter.ToString();
	}

	private void _on_Switch_Scene_pressed()
	{
		GetTree().ChangeScene("res://79Nebula/Scenes/Battle.tscn");
	}

}
