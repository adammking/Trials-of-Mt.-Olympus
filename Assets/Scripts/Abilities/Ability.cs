using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot(ElementName = "ability")]
public class Ability
{

	[XmlElement(ElementName = "description")]
	public string Description { get; set; }
	[XmlElement(ElementName = "dmg")]
	public int Damage { get; set; }
	[XmlElement(ElementName = "tickRate")]
	public float TickRate { get; set; }
	[XmlElement(ElementName = "coolDown")]
	public float CoolDown { get; set; }
	[XmlElement(ElementName = "abilityLength")]
	public float AbilityLength { get; set; }
	[XmlElement(ElementName = "isAoe")]
	public bool IsAoe { get; set; }
	[XmlAttribute(AttributeName = "code")]
	public string Code { get; set; }
	[XmlAttribute(AttributeName = "name")]
	public string Name { get; set; }

	public float CurrentCooldown;

}
