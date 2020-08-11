using Godot;
using Nebula._79Nebula.DataAccess;
using Nebula._79Nebula.Enums;
using Nebula._79Nebula.Models;
using System;
using System.Collections.Generic;

public class Battle_Button : Node
{
	private BattleInstance _battle;
	private Player _player;

	private Label _playerLabel;
	private Label _opponentLabel;
	private Label _battleLabel;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = new Player("Test Player", 1, 1, 1, new List<string>() {
				"Default1", "Default2", "Default3", "Default4", "Default5"
			});

		string _file = @"79Nebula\Data\Enemies.json";
		string json = System.IO.File.ReadAllText(_file);
		Enemy_DataAccess eda = Enemy_DataAccess.FromJson(json);
		List<Enemy> enemies = eda.Enemies;

		Console.WriteLine("Enemies: ");
		foreach(Enemy e in enemies)
		{
			Console.WriteLine($"\t{e.Name}");
		}

		_battle = new BattleInstance(_player, enemies);

		_playerLabel = GetNode<Label>("../PlayerName_Label");
		_opponentLabel = GetNode<Label>("../OpponentName_Label");
		_battleLabel = GetNode<Label>("../BattleResult_Label");
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	private void _on_Button_pressed()
	{
		AutoBattleState result = _battle.Battle();
		_battleLabel.Text = result.ToString();
		_playerLabel.Text = $"{_player.Name} ({_player.Health})";
		_opponentLabel.Text = $"{_battle.Opponent.Name} ({_battle.Opponent.Health})";
		Console.WriteLine($"Battle Result: \n\t{result}\n");
		Console.WriteLine($"Player: \n\t{_player}\n");
		Console.WriteLine($"Opponent: \n\t{_battle.Opponent}\n");
	}
}



