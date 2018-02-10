using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("RessourceCollection")]
public class RessourceContainer{
	[XmlArray("Ressources")]
	[XmlArrayItem("Ressource")]
	public Ressource[] ressources;

	public void saveTest(RessourceContainer ressource){
		XmlSerializer serializer = new XmlSerializer (typeof(RessourceContainer));
		using (FileStream stream = new FileStream ("Test.xml", FileMode.Create)) {
			serializer.Serialize (stream, ressource);
		}
	}

	public static RessourceContainer Load(){
		XmlSerializer serializer = new XmlSerializer (typeof(RessourceContainer));
		using (FileStream stream = new FileStream ("Test.xml", FileMode.Open)) {
			return serializer.Deserialize (stream) as RessourceContainer;
		}
	}
}
