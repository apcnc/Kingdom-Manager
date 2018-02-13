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

	public void Save(RessourceContainer ressource, string filename){
		XmlSerializer serializer = new XmlSerializer (typeof(RessourceContainer));

		using (FileStream stream = new FileStream (filename, FileMode.Create)) {
			serializer.Serialize (stream, ressource);
			}

	}

	public static RessourceContainer Load(string filename){
		XmlSerializer serializer = new XmlSerializer (typeof(RessourceContainer));
		try{
			using (FileStream stream = new FileStream (filename, FileMode.Open)) {
				return serializer.Deserialize (stream) as RessourceContainer;
				}
		}
		catch(IOException exeption){
			using (FileStream stream = new FileStream ("standard.xml", FileMode.Open)) {
				RessourceContainer ressources = serializer.Deserialize (stream) as RessourceContainer;
				ressources.Save (ressources,filename);
				return ressources;
			}
		}
	}
}
