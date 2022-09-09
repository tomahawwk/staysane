using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad : MonoBehaviour
{
	public GameObject player;

	public Text slot1;
	public Text slot2;
	Button currentButton;

	[System.Serializable]

	public class StringName {
		public string saveTitle;
		public float x;
		public float y;
		public float z;
	}

	public void init(){
		if(File.Exists(Application.dataPath + "/saves/save.bot")){
			FileStream fs = new FileStream(Application.dataPath + "/saves/save.bot", FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			StringName stringName = (StringName)formatter.Deserialize(fs);
			slot1.text = "Сохранение " + stringName.saveTitle;
			fs.Close();
		}

		if(File.Exists(Application.dataPath + "/saves/save1.bot")){
			FileStream fs = new FileStream(Application.dataPath + "/saves/save.bot", FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			StringName stringName = (StringName)formatter.Deserialize(fs);
			slot2.text = "Сохранение " + stringName.saveTitle;
			fs.Close();
		}
	}

	public void Saves(){
		SlotConfig slotConfig = currentButton.GetComponent<SlotConfig>();
		slotConfig.busy = true;
		StringName stringName = new StringName();
		stringName.saveTitle = slotConfig.number.ToString();
        stringName.x = player.transform.position.x;
		stringName.y = player.transform.position.y;
		stringName.z = player.transform.position.z;
		if(!File.Exists(Application.dataPath + "saves")){
			Directory.CreateDirectory(Application.dataPath + "/saves");
			if(slotConfig.number == 1){
				FileStream fs = new FileStream(Application.dataPath + "/saves/save.bot", FileMode.Create);
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(fs, stringName);
				slot1.text = "Сохранение " + stringName.saveTitle;
				fs.Close();
			}
			if(slotConfig.number == 2){
				FileStream fs = new FileStream(Application.dataPath + "/saves/save.bot", FileMode.Create);
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(fs, stringName);
				slot2.text = "Сохранение " + stringName.saveTitle;
				fs.Close();
			}
		}
	}

	public void Loads(){
		SlotConfig slotConfig = currentButton.GetComponent<SlotConfig>();
		if(slotConfig.number == 1){
			if(File.Exists(Application.dataPath + "/saves/save.bot")){
				FileStream fs = new FileStream(Application.dataPath + "/saves/save.bot", FileMode.Open);
				BinaryFormatter formatter = new BinaryFormatter();
				StringName stringName = (StringName)formatter.Deserialize(fs);
				player.transform.position = new Vector3(stringName.x, stringName.y, stringName.z);
				fs.Close();
			}
		}
		if(slotConfig.number == 2){
			if(File.Exists(Application.dataPath + "/saves/save1.bot")){
				FileStream fs = new FileStream(Application.dataPath + "/saves/save1.bot", FileMode.Open);
				BinaryFormatter formatter = new BinaryFormatter();
				StringName stringName = (StringName)formatter.Deserialize(fs);
				player.transform.position = new Vector3(stringName.x, stringName.y, stringName.z);
				fs.Close();
			}
		}
	}

	public void Deleted(){
		SlotConfig slotConfig = currentButton.GetComponent<SlotConfig>();
		if(slotConfig.number == 1){
			if(File.Exists(Application.dataPath + "/saves/save.bot")){
				File.Delete(Application.dataPath + "/saves/save.bot");
				slot1.text = "Пустой слот";
			}
		}
		if(slotConfig.number == 2){
			if(File.Exists(Application.dataPath + "/saves/save1.bot")){
				File.Delete(Application.dataPath + "/saves/save1.bot");
				slot2.text = "Пустой слот";
			}
		}
	}

	public void ClickSlot(Button button){
		currentButton = button;
		Debug.Log(currentButton);
	}
}
