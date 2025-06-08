using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;


[System.Serializable]
public class WordList
{
    public List<string> words;  // Daftar kata-kata untuk setiap level
}

[System.Serializable]
public class Level
{
    public string levelName;   // Nama level (misalnya, Level 1, Level 2, dsb)
    public WordList wordList;  // List kata-kata untuk level ini
}

public class WordBank : MonoBehaviour
{
    [SerializeField] private List<Level> levels = new List<Level>();
    private Queue<string> bossWordsQueue = new Queue<string>();
    private void Awake()
    {
        if (levels.Count == 0)
        {
            levels.Add(new Level
            {
                levelName = "Level 1",
                wordList = new WordList
                {
                    words = new List<string> { "Stone", "Cave", "Bone", "Flint", "Axe", "Clay", "Hunt", "Fire", "Tribe", "Spear", "Rock", "Tool", "Club", "Hut", "Fur", "Hide", "Clan", "Drum", "Ochre", "Nomad", "Arrow", "Bow", "Pot", "Prey", "Shell", "Wood", "River", "Lake", "Hill", "Forest", "Grass", "Herb", "Root", "Seed", "Berry", "Meat", "Fish", "Skin", "Tent", "Chief", "Elder", "Shaman", "Child", "Family", "Path", "Trail", "Dust", "Mud", "Sand", "Sky", "Sun", "Moon", "Star", "Dawn", "Dusk", "Grave", "Myth", "Dance", "Song", "Ritual" }
                }
            });
            levels.Add(new Level
            {
                levelName = "Level 2",
                wordList = new WordList
                {
                    words = new List<string> { "Pharaoh", "Ziggurat", "Sumerian", "Papyrus", "Cuneiform", "Trilobite", "Mastodon", "Aqueduct", "Sarcophagus", "Megalith", "Pyramid", "Sphinx", "Mummy", "Chariot", "Parthenon", "Gladiator", "Colosseum", "Acropolis", "Obelisk", "Abacus", "Legion", "Senate", "Toga", "Fossil", "Amber", "Agora", "Forum", "Villa", "Scroll", "Tablet", "Stylus", "Mosaic", "Fresco", "Column", "Arch", "Dome", "Viaduct", "Amphitheater", "Circus", "Oracle", "Philosopher", "Emperor", "Centurion", "Consul", "Patrician", "Plebeian", "Trireme", "Ballista", "Catapult", "Hoplite", "Phalanx", "Vellum", "Parchment", "Necropolis", "Hypostyle", "Pylon", "Cartouche", "Bronze", "Iron", "Pottery" }
                }
            });
            levels.Add(new Level
            {
                levelName = "Level 3",
                wordList = new WordList
                {
                    words = new List<string> { "Akkadian", "Mycenaean", "Ostracon", "Neanderthal", "Hieroglyph", "Chthonic", "Amphora", "Urartu", "Olmec", "Hominid", "Babylonian", "Assyrian", "Hittite", "Minoan", "Phoenician", "Etruscan", "Scarab", "Canopic", "Celt", "Druid", "Pict", "Gaul", "Gnostic", "Zoroaster", "Thracian", "Scythian", "Parthian", "Carthaginian", "Numidian", "Sarmatian", "Lapita", "Nok", "Kushite", "Axumite", "Sabaean", "Nabatean", "Achaemenid", "Seleucid", "Ptolemaic", "Hellenistic", "Samnite", "Villanovan", "Hallstatt", "La Tene", "Megaron", "Tholos", "Libation", "Augur", "Haruspex", "Vestal", "Satrap", "Censor", "Tribune", "Equites", "Ostracism", "Petroglyph", "Geoglyph", "Quipu", "Codex", "Stele" }
                }
            });
            levels.Add(new Level
            {
                levelName = "Level 4",
                wordList = new WordList
                {
                    words = new List<string> { "Acheulean", "Mousterian", "Ichthyosaur", "Pterosaur", "Devonian", "Silurian", "Therapsid", "Propliopithecus", "Archaeology", "Glyptodon", "Pleistocene", "Pliocene", "Holocene", "Cambrian", "Permian", "Triassic", "Cretaceous", "Megalodon", "Smilodon", "Iguanodon", "Stegosaurus", "Ankylosaurus", "Triceratops", "Brontosaurus", "Megaloceros", "Ordovician", "Jurassic", "Paleocene", "Eocene", "Oligocene", "Miocene", "Carboniferous", "Proterozoic", "Archaean", "Hadean", "Hadrosaur", "Ceratopsian", "Sauropod", "Theropod", "Ornithopod", "Pteranodon", "Mosasaur", "Plesiosaur", "Ardipithecus", "Paranthropus", "Sahelanthropus", "Orrorin", "Kenyanthropus", "Homo Habilis", "Homo Erectus", "Denisovan", "Megafauna", "Stratigraphy", "Dendrochronology", "Microlith", "Biface", "Handaxe", "Aurignacian", "Magdalenian", "Gravettian" }
                }
            });
        }

        foreach (var level in levels)
        {
            Shuffle(level.wordList.words);
            ConverLowerCase(level.wordList.words);
        }


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

    public string GetWord(int level)
    {
        if (level <= 0 || level > levels.Count)
            return "default";

        // Ambil list kata untuk level yang sesuai
        List<string> selectedList = levels[level - 1].wordList.words;

        return selectedList[Random.Range(0, selectedList.Count)];
    }

    public string GetRandomBossWord()
    {
        List<string> allWords = new List<string>();
        foreach (var level in levels)
        {
            allWords.AddRange(level.wordList.words);
        }

        return allWords[Random.Range(0, allWords.Count)];
    }

    public void GenerateBossWords(int count)
    {
        bossWordsQueue.Clear();
        for (int i = 0; i < count; i++)
        {
            bossWordsQueue.Enqueue(GetRandomBossWord());
        }
    }

    public string GetNextBossWord()
    {
        if (bossWordsQueue.Count > 0)
            return bossWordsQueue.Dequeue();

        return null; // artinya boss sudah selesai
    }
}
