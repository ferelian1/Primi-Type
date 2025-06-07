using System.Linq; 
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.ComponentModel;
using Unity.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

public class WordBank : MonoBehaviour
{
    [SerializeField] private List<string> wave1Words = new List<string>() { "Stone", "Cave", "Bone", "Flint", "Axe", "Clay", "Hunt", "Fire", "Tribe", "Spear", "Rock", "Tool", "Club", "Hut", "Fur", "Hide", "Clan", "Drum", "Ochre", "Nomad", "Arrow", "Bow", "Pot", "Prey", "Shell" };
    [SerializeField] private List<string> wave2Words = new List<string>() { "Pharaoh", "Ziggurat", "Sumerian", "Papyrus", "Cuneiform", "Trilobite", "Mastodon", "Aqueduct", "Sarcophagus", "Megalith", "Pyramid", "Sphinx", "Mummy", "Chariot", "Parthenon", "Gladiator", "Colosseum", "Acropolis", "Obelisk", "Abacus", "Legion", "Senate", "Toga", "Fossil", "Amber" };
    [SerializeField] private List<string> wave3Words = new List<string>() { "Akkadian", "Mycenaean", "Ostracon", "Neanderthal", "Hieroglyph", "Chthonic", "Amphora", "Urartu", "Olmec", "Hominid", "Babylonian", "Assyrian", "Hittite", "Minoan", "Phoenician", "Etruscan", "Scarab", "Canopic", "Celt", "Druid", "Pict", "Gaul", "Gnostic", "Zoroaster", "Thracian" };
    [SerializeField] private List<string> wave4Words = new List<string>() { "Acheulean", "Mousterian", "Ichthyosaur", "Pterosaur", "Devonian", "Silurian", "Therapsid", "Propliopithecus", "Archaeology", "Glyptodon", "Pleistocene", "Pliocene", "Holocene", "Cambrian", "Permian", "Triassic", "Cretaceous", "Megalodon", "Smilodon", "Iguanodon", "Stegosaurus", "Ankylosaurus", "Triceratops", "Brontosaurus", "Megaloceros" };

    
    private void Awake()
    {
        
        Shuffle(wave1Words);
        Shuffle(wave2Words);
        Shuffle(wave3Words);
        Shuffle(wave4Words);
        ConverLowerCase(wave1Words);
        ConverLowerCase(wave2Words);
        ConverLowerCase(wave3Words);
        ConverLowerCase(wave4Words);

    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];

            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConverLowerCase(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetWord(int waveLevel)
    {
        List<string> selectedList = wave1Words;

        switch (waveLevel)
        {
            case 1: selectedList = wave1Words; break;
            case 2: selectedList = wave2Words; break;
            case 3: selectedList = wave3Words; break;
            case 4: selectedList = wave4Words; break;
        }

        if (selectedList.Count > 0)
        {
            return selectedList[Random.Range(0, selectedList.Count)];
        }

        return "default";
    }

    public string GetRandomBossWord()
    {
        List<string> allWords = new List<string>();
        allWords.AddRange(wave1Words);
        allWords.AddRange(wave2Words);
        allWords.AddRange(wave3Words);
        allWords.AddRange(wave4Words);

        return allWords[Random.Range(0, allWords.Count)];
    }
    

}
