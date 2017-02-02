﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;

/// <summary>
/// This class manages the UI elements of player characters.
/// Author: Richard Brönnimann
/// </summary>
public class UIScript : MonoBehaviour
{
    public GameObject UICanvas;

    // Health UI
    public GameObject Player1HealthUI_background;
    public GameObject Player1HealthUI_fill;
    public GameObject Player1HealthUI_outline;
    public GameObject Player2HealthUI_background;
    public GameObject Player2HealthUI_fill;
    public GameObject Player2HealthUI_outline;
    public GameObject Player3HealthUI_background;
    public GameObject Player3HealthUI_fill;
    public GameObject Player3HealthUI_outline;
    public GameObject Player4HealthUI_background;
    public GameObject Player4HealthUI_fill;
    public GameObject Player4HealthUI_outline;
    public Image TeamHealthBar;
    public Image TeamHealthBar_Border;
    public Sprite TeamHealthActive;
    public Sprite TeamHealthInactive;

    // Weapon UI
    public GameObject P1Weapon_background;
    public GameObject P1Weapon_border;
    public GameObject P1Weapon_icon;
    public GameObject P1WeaponHeat;
    public GameObject P2Weapon_background;
    public GameObject P2Weapon_border;
    public GameObject P2Weapon_icon;
    public GameObject P2WeaponHeat;
    public GameObject P3Weapon_background;
    public GameObject P3Weapon_border;
    public GameObject P3Weapon_icon;
    public GameObject P3WeaponHeat;
    public GameObject P4Weapon_background;
    public GameObject P4Weapon_border;
    public GameObject P4Weapon_icon;
    public GameObject P4WeaponHeat;

    // Abilities
    public GameObject[] EnergyBars;

    // Other
    public GameObject IndicatorPlaneOne;
    public GameObject IndicatorPlaneTwo;
    public GameObject IndicatorPlaneThree;
    public GameObject IndicatorPlaneFour;

    private GameObject game;
    private GameObject P1IndicatorPlane;
    private GameObject P2IndicatorPlane;
    private GameObject P3IndicatorPlane;
    private GameObject P4IndicatorPlane;
    private GameObject Indicate;
    private GameObject Indicate2;
    private GameObject Indicate3;
    private GameObject Indicate4;
    private float teamhealthAmount;
    private float maxTeamHealth;
    private float maxPlayerHealth;
    private Image player1HealthBar_1;
    private Image player1HealthBar_2;
    private Image player1HealthBar_3;
    private Image player1Heat;
    private Image player1Weapon_1;
    private Image player1Weapon_2;
    private Image player1Weapon_3;
    private Image player2HealthBar_1;
    private Image player2HealthBar_2;
    private Image player2HealthBar_3;
    private Image player2Heat;
    private Image player2Weapon_1;
    private Image player2Weapon_2;
    private Image player2Weapon_3;
    private Image player3HealthBar_1;
    private Image player3HealthBar_2;
    private Image player3HealthBar_3;
    private Image player3Heat;
    private Image player3Weapon_1;
    private Image player3Weapon_2;
    private Image player3Weapon_3;
    private Image player4HealthBar_1;
    private Image player4HealthBar_2;
    private Image player4HealthBar_3;
    private Image player4Heat;
    private Image player4Weapon_1;
    private Image player4Weapon_2;
    private Image player4Weapon_3;
    private bool isHealing = false;
    private List<GameObject> CurrentPlayers;
    private Image[] AbilityBars = new Image[12];

    // Use this for initialization
    private void Start()
    {
        game = GameObject.Find("Game");
        GameInspector gameInspector = game.GetComponent<GameInspector>();
        maxTeamHealth = gameInspector.MaxTeamHealth;
        GameObject playerPrefab = GameObject.Find("Player");
        DamageAbleObject playerDamageable = gameInspector.PlayerPrefab.GetComponent<DamageAbleObject>();
        maxPlayerHealth = playerDamageable.MaxHealth;
    }

    // Update is called once per frame
    private void Update()
    {

        if (CurrentPlayers != null)
        {
            foreach (GameObject player in CurrentPlayers)
            {
                if (player != null)
                    UpdateHealth(player.GetComponent<Player>(), player.GetComponent<DamageAbleObject>());
            }

            if (UICanvas.activeInHierarchy == false)
            {
                UICanvas.SetActive(true);
            }

            teamhealthAmount = Player.TeamHealth / maxTeamHealth;
            TeamHealthBar.fillAmount = teamhealthAmount;

            if (isHealing)
            {
                if (TeamHealthBar != null && TeamHealthActive != null)
                {
                    Image image = TeamHealthBar_Border.GetComponent<Image>();
                    image.sprite = TeamHealthActive;
                }
            }
            else
            {
                if (TeamHealthBar != null && TeamHealthInactive != null)
                {
                    Image image = TeamHealthBar_Border.GetComponent<Image>();
                    image.sprite = TeamHealthInactive;
                }
            }
            isHealing = false;
        }
        else
        {
            UICanvas.SetActive(false);
        }
    }

    public void OnSpawn(PlayerIndex index)
    {
        CreateUI(index, UICanvas.transform);
        CurrentPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }

    public void OnExit(PlayerIndex index)
    {
        CurrentPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        RemoveUI(index);
    }

    public void ActivateTeamBar()
    {
        isHealing = true;
    }

    private void UpdateHealth(Player player, DamageAbleObject damageAbleObject)
    {
        switch (player.Index)
        {
            case PlayerIndex.One:
                {
                    player1HealthBar_2.fillAmount = damageAbleObject.Health / maxPlayerHealth;
                    player1Heat.fillAmount = player.PrimaryHeat / player.PrimaryMaxHeat;

                    for (int j = 0; j < 3; j++)
                    {
                        AbilityBars[j].fillAmount = player.AbilityEnergy(j + 1) / player.MaxEnergy;
                    }

                    if (Indicate == null)
                    {
                        Indicate = Instantiate(P1IndicatorPlane);
                    }
                    Indicate.transform.SetParent(player.transform, false);
                }
                break;

            case PlayerIndex.Two:
                {
                    player2HealthBar_2.fillAmount = damageAbleObject.Health / maxPlayerHealth;
                    player2Heat.fillAmount = player.PrimaryHeat / player.PrimaryMaxHeat;

                    for (int j = 3; j < 6; j++)
                    {
                        AbilityBars[j].fillAmount = player.AbilityEnergy(j - 2) / player.MaxEnergy;
                    }

                    if (Indicate2 == null)
                    {
                        Indicate2 = Instantiate(P2IndicatorPlane);
                    }
                    Indicate2.transform.SetParent(player.transform, false);
                }
                break;

            case PlayerIndex.Three:
                {
                    player3HealthBar_2.fillAmount = damageAbleObject.Health / maxPlayerHealth;
                    player3Heat.fillAmount = player.PrimaryHeat / player.PrimaryMaxHeat;

                    for (int j = 6; j < 9; j++)
                    {
                        AbilityBars[j].fillAmount = player.AbilityEnergy(j - 5) / player.MaxEnergy;
                    }

                    if (Indicate3 == null)
                    {
                        Indicate3 = Instantiate(P3IndicatorPlane);
                    }
                    Indicate3.transform.SetParent(player.transform, false);
                }
                break;

            case PlayerIndex.Four:
                {
                    player4HealthBar_2.fillAmount = damageAbleObject.Health / maxPlayerHealth;
                    player4Heat.fillAmount = player.PrimaryHeat / player.PrimaryMaxHeat;

                    for (int j = 9; j < 12; j++)
                    {
                        AbilityBars[j].fillAmount = player.AbilityEnergy(j - 8) / player.MaxEnergy;
                    }

                    if (Indicate4 == null)
                    {
                        Indicate4 = Instantiate(P4IndicatorPlane);
                    }
                    Indicate4.transform.SetParent(player.transform, false);
                }
                break;

            default:
                break;
        }
    }

    private void RemoveUI(PlayerIndex index)
    {
        switch (index)
        {
            case PlayerIndex.One:
                {
                    Destroy(player1HealthBar_1.gameObject);
                    Destroy(player1HealthBar_2.gameObject);
                    Destroy(player1HealthBar_3.gameObject);
                    Destroy(player1Heat.gameObject);
                    Destroy(player1Weapon_1.gameObject);
                    Destroy(player1Weapon_2.gameObject);
                    Destroy(player1Weapon_3.gameObject);

                    for (int k = 0; k < 3; k++)
                    {
                        Destroy(AbilityBars[k].gameObject);
                    }
                }
                break;
            case PlayerIndex.Two:
                {
                    Destroy(player2HealthBar_1.gameObject);
                    Destroy(player2HealthBar_2.gameObject);
                    Destroy(player2HealthBar_3.gameObject);
                    Destroy(player2Heat.gameObject);
                    Destroy(player2Weapon_1.gameObject);
                    Destroy(player2Weapon_2.gameObject);
                    Destroy(player2Weapon_3.gameObject);

                    for (int k = 3; k < 6; k++)
                    {
                        Destroy(AbilityBars[k].gameObject);
                    }
                }
                break;
            case PlayerIndex.Three:
                {
                    Destroy(player3HealthBar_1.gameObject);
                    Destroy(player3HealthBar_2.gameObject);
                    Destroy(player3HealthBar_3.gameObject);
                    Destroy(player3Heat.gameObject);
                    Destroy(player3Weapon_1.gameObject);
                    Destroy(player3Weapon_2.gameObject);
                    Destroy(player3Weapon_3.gameObject);

                    for (int k = 6; k < 9; k++)
                    {
                        Destroy(AbilityBars[k].gameObject);
                    }
                }
                break;
            case PlayerIndex.Four:
                {
                    Destroy(player4HealthBar_1.gameObject);
                    Destroy(player4HealthBar_2.gameObject);
                    Destroy(player4HealthBar_3.gameObject);
                    Destroy(player4Heat.gameObject);
                    Destroy(player4Weapon_1.gameObject);
                    Destroy(player4Weapon_2.gameObject);
                    Destroy(player4Weapon_3.gameObject);

                    for (int k = 9; k < 12; k++)
                    {
                        Destroy(AbilityBars[k].gameObject);
                    }
                }
                break;

            default:
                break;
        };
    }

    private void CreateUI(PlayerIndex index, Transform UICanvas)
    {

        switch (index)
        {
            case PlayerIndex.One:
                {
                    GameObject P1_1 = Instantiate(Player1HealthUI_background);
                    player1HealthBar_1 = P1_1.GetComponent<Image>();
                    player1HealthBar_1.transform.SetParent(UICanvas, false);
                    GameObject P1_2 = Instantiate(Player1HealthUI_fill);
                    player1HealthBar_2 = P1_2.GetComponent<Image>();
                    player1HealthBar_2.transform.SetParent(UICanvas, false);
                    GameObject P1_3 = Instantiate(Player1HealthUI_outline);
                    player1HealthBar_3 = P1_3.GetComponent<Image>();
                    player1HealthBar_3.transform.SetParent(UICanvas, false);
                    P1IndicatorPlane = IndicatorPlaneOne;
                    GameObject Heat1_1 = Instantiate(P1Weapon_background);
                    player1Weapon_1 = Heat1_1.GetComponent<Image>();
                    player1Weapon_1.transform.SetParent(UICanvas, false);
                    GameObject Heat1_2 = Instantiate(P1Weapon_icon);
                    player1Weapon_2 = Heat1_2.GetComponent<Image>();
                    player1Weapon_2.transform.SetParent(UICanvas, false);
                    GameObject Heat1_3 = Instantiate(P1WeaponHeat);
                    player1Heat = Heat1_3.GetComponent<Image>();
                    player1Heat.transform.SetParent(UICanvas, false);
                    GameObject Heat1_4 = Instantiate(P1Weapon_border);
                    player1Weapon_3 = Heat1_4.GetComponent<Image>();
                    player1Weapon_3.transform.SetParent(UICanvas, false);

                    for (int i = 0; i < 3; i++)
                    {
                        GameObject Energy = Instantiate(EnergyBars[i]);
                        AbilityBars[i] = Energy.GetComponent<Image>();
                        AbilityBars[i].transform.SetParent(UICanvas, false);
                    }
                }
                break;
            case PlayerIndex.Two:
                {
                    GameObject P2_1 = Instantiate(Player2HealthUI_background);
                    player2HealthBar_1 = P2_1.GetComponent<Image>();
                    player2HealthBar_1.transform.SetParent(UICanvas, false);
                    GameObject P2_2 = Instantiate(Player2HealthUI_fill);
                    player2HealthBar_2 = P2_2.GetComponent<Image>();
                    player2HealthBar_2.transform.SetParent(UICanvas, false);
                    GameObject P2_3 = Instantiate(Player2HealthUI_outline);
                    player2HealthBar_3 = P2_3.GetComponent<Image>();
                    player2HealthBar_3.transform.SetParent(UICanvas, false);
                    P2IndicatorPlane = IndicatorPlaneTwo;
                    GameObject Heat2_1 = Instantiate(P2Weapon_background);
                    player2Weapon_1 = Heat2_1.GetComponent<Image>();
                    player2Weapon_1.transform.SetParent(UICanvas, false);
                    GameObject Heat2_2 = Instantiate(P2Weapon_icon);
                    player2Weapon_2 = Heat2_2.GetComponent<Image>();
                    player2Weapon_2.transform.SetParent(UICanvas, false);
                    GameObject Heat2_3 = Instantiate(P2WeaponHeat);
                    player2Heat = Heat2_3.GetComponent<Image>();
                    player2Heat.transform.SetParent(UICanvas, false);
                    GameObject Heat2_4 = Instantiate(P2Weapon_border);
                    player2Weapon_3 = Heat2_4.GetComponent<Image>();
                    player2Weapon_3.transform.SetParent(UICanvas, false);

                    for (int i = 3; i < 6; i++)
                    {
                        GameObject Energy = Instantiate(EnergyBars[i]);
                        AbilityBars[i] = Energy.GetComponent<Image>();
                        AbilityBars[i].transform.SetParent(UICanvas, false);
                    }
                }
                break;
            case PlayerIndex.Three:
                {
                    GameObject P3_1 = Instantiate(Player3HealthUI_background);
                    player3HealthBar_1 = P3_1.GetComponent<Image>();
                    player3HealthBar_1.transform.SetParent(UICanvas, false);
                    GameObject P3_2 = Instantiate(Player3HealthUI_fill);
                    player3HealthBar_2 = P3_2.GetComponent<Image>();
                    player3HealthBar_2.transform.SetParent(UICanvas, false);
                    GameObject P3_3 = Instantiate(Player3HealthUI_outline);
                    player3HealthBar_3 = P3_3.GetComponent<Image>();
                    player3HealthBar_3.transform.SetParent(UICanvas, false);
                    P3IndicatorPlane = IndicatorPlaneThree;
                    GameObject Heat3_1 = Instantiate(P3Weapon_background);
                    player3Weapon_1 = Heat3_1.GetComponent<Image>();
                    player3Weapon_1.transform.SetParent(UICanvas, false);
                    GameObject Heat3_2 = Instantiate(P3Weapon_icon);
                    player3Weapon_2 = Heat3_2.GetComponent<Image>();
                    player3Weapon_2.transform.SetParent(UICanvas, false);
                    GameObject Heat3_3 = Instantiate(P3WeaponHeat);
                    player3Heat = Heat3_3.GetComponent<Image>();
                    player3Heat.transform.SetParent(UICanvas, false);
                    GameObject Heat3_4 = Instantiate(P3Weapon_border);
                    player3Weapon_3 = Heat3_4.GetComponent<Image>();
                    player3Weapon_3.transform.SetParent(UICanvas, false);

                    for (int i = 6; i < 9; i++)
                    {
                        GameObject Energy = Instantiate(EnergyBars[i]);
                        AbilityBars[i] = Energy.GetComponent<Image>();
                        AbilityBars[i].transform.SetParent(UICanvas, false);
                    }
                }
                break;
            case PlayerIndex.Four:
                {
                    GameObject P4_1 = Instantiate(Player4HealthUI_background);
                    player4HealthBar_1 = P4_1.GetComponent<Image>();
                    player4HealthBar_1.transform.SetParent(UICanvas, false);
                    GameObject P4_2 = Instantiate(Player4HealthUI_fill);
                    player4HealthBar_2 = P4_2.GetComponent<Image>();
                    player4HealthBar_2.transform.SetParent(UICanvas, false);
                    GameObject P4_3 = Instantiate(Player4HealthUI_outline);
                    player4HealthBar_3 = P4_3.GetComponent<Image>();
                    player4HealthBar_3.transform.SetParent(UICanvas, false);
                    P4IndicatorPlane = IndicatorPlaneFour;
                    GameObject Heat4_1 = Instantiate(P4Weapon_background);
                    player4Weapon_1 = Heat4_1.GetComponent<Image>();
                    player4Weapon_1.transform.SetParent(UICanvas, false);
                    GameObject Heat4_2 = Instantiate(P4Weapon_icon);
                    player4Weapon_2 = Heat4_2.GetComponent<Image>();
                    player4Weapon_2.transform.SetParent(UICanvas, false);
                    GameObject Heat4_3 = Instantiate(P4WeaponHeat);
                    player4Heat = Heat4_3.GetComponent<Image>();
                    player4Heat.transform.SetParent(UICanvas, false);
                    GameObject Heat4_4 = Instantiate(P4Weapon_border);
                    player4Weapon_3 = Heat4_4.GetComponent<Image>();
                    player4Weapon_3.transform.SetParent(UICanvas, false);

                    for (int i = 9; i < 12; i++)
                    {
                        GameObject Energy = Instantiate(EnergyBars[i]);
                        AbilityBars[i] = Energy.GetComponent<Image>();
                        AbilityBars[i].transform.SetParent(UICanvas, false);
                    }
                }
                break;

            default:
                break;
        }
    }
}