using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject CharactorStatePanel;
    public GameObject CharactorUpgradePanel;
	public GameObject SkillPanel;
    public GameObject GameOptionPanel;

	public GameObject ListPanel;
	public GameObject ListActiveImage;

	private void Start()
	{
		CharactorStatePanel.transform.parent = ListActiveImage.transform;
		CharactorUpgradePanel.transform.parent = ListPanel.transform;
		GameOptionPanel.transform.parent = ListPanel.transform;
		SkillPanel.transform.parent = ListPanel.transform;

		CharactorStatePanel.SetActive(true);
		CharactorUpgradePanel.SetActive(false);
		GameOptionPanel.SetActive(false);
		SkillPanel.SetActive(true);
	}

	public void StateButton()
    {
		Managers.Sound.Play("Effect/UI/MenuButton");
		CharactorStatePanel.transform.parent = ListActiveImage.transform;
		CharactorUpgradePanel.transform.parent = ListPanel.transform;
		GameOptionPanel.transform.parent = ListPanel.transform;
		SkillPanel.transform.parent = ListPanel.transform;
		
		CharactorStatePanel.SetActive(true);
		CharactorUpgradePanel.SetActive(false);
		GameOptionPanel.SetActive(false);
	}

	public void UpgradeButton()
	{
		Managers.Sound.Play("Effect/UI/MenuButton");
		CharactorUpgradePanel.transform.parent = ListActiveImage.transform;
		CharactorStatePanel.transform.parent = ListPanel.transform;
		GameOptionPanel.transform.parent = ListPanel.transform;
		SkillPanel.transform.parent = ListPanel.transform;
		
		CharactorStatePanel.SetActive(false);
		CharactorUpgradePanel.SetActive(true);
		GameOptionPanel.SetActive(false);
	}

	public void GameOptionButton()
    {
		Managers.Sound.Play("Effect/UI/MenuButton");
		GameOptionPanel.transform.parent = ListActiveImage.transform;
		CharactorStatePanel.transform.parent = ListPanel.transform;
		CharactorUpgradePanel.transform.parent = ListPanel.transform;
		SkillPanel.transform.parent = ListPanel.transform;
		
		CharactorStatePanel.SetActive(false);
		CharactorUpgradePanel.SetActive(false);
		GameOptionPanel.SetActive(true);
	}

	public void SkillButton()
	{
		Managers.Sound.Play("Effect/UI/MenuButton");
		SkillPanel.transform.parent = ListActiveImage.transform;
		CharactorStatePanel.transform.parent = ListPanel.transform;
		CharactorUpgradePanel.transform.parent = ListPanel.transform;
		GameOptionPanel.transform.parent = ListPanel.transform;
		
		CharactorStatePanel.SetActive(false);
		CharactorUpgradePanel.SetActive(false);
		GameOptionPanel.SetActive(false);
	}

}
