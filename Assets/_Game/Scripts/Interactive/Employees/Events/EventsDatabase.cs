using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Game.Scripts.Interactive.Employees.Events
{
    [CreateAssetMenu(
        fileName = "EventsDatabase",
        menuName = "Employees/Events Database"
    )]
    public class EventsDatabase : ScriptableObject
    {
        [field: SerializeField] public List<EventConfig> Events { get; private set; }

#if UNITY_EDITOR
        [ContextMenu("Save Events to JSON")]
        private void SaveEventsToJson()
        {
            // Путь для сохранения (по умолчанию — папка проекта)
            string path = EditorUtility.SaveFilePanel(
                "Сохранить события в JSON",
                Application.dataPath,
                "EventsDatabase.json",
                "json"
            );

            if (string.IsNullOrEmpty(path))
                return;

            // Собираем DTO-список
            var dtoList = new List<EventConfigDto>(Events.Count);
            foreach (var ev in Events)
            {
                if (ev == null) continue;
                dtoList.Add(EventConfigDto.FromConfig(ev));
            }

            var wrapper = new EventsDatabaseDto { Events = dtoList };

            // ToJson требует корневой объект
            string json = JsonUtility.ToJson(wrapper, prettyPrint: true);
            File.WriteAllText(path, json);

            // Обновляем дерево ассетов, если файл сохранён внутри Assets
            if (path.StartsWith(Application.dataPath))
                AssetDatabase.Refresh();

            Debug.Log($"[EventsDatabase] Сохранено {dtoList.Count} событий: {path}");
        }
#endif
    }

    // ===== DTO-классы для корректной JSON-сериализации =====
    // JsonUtility не сериализует свойства с { get; private set; }
    // и ссылки на Unity-объекты, поэтому используем обычные поля.

    [System.Serializable]
    public class EventsDatabaseDto
    {
        public List<EventConfigDto> Events;
    }

    [System.Serializable]
    public class EventConfigDto
    {
        public string Name;
        public float ProgressScale;
        public int ColleaguesAmount;
        public bool Kill;
        public bool Leave;
        public List<TraitReactionDto> Reactions;

        public static EventConfigDto FromConfig(EventConfig cfg)
        {
            var dto = new EventConfigDto
            {
                Name = cfg.Name,
                ProgressScale = cfg.ProgressScale,
                ColleaguesAmount = cfg.ColleaguesAmount,
                Kill = cfg.Kill,
                Leave = cfg.Leave,
                Reactions = new List<TraitReactionDto>()
            };

            if (cfg.Reactions != null)
            {
                foreach (var r in cfg.Reactions)
                {
                    if (r == null) continue;
                    dto.Reactions.Add(TraitReactionDto.FromReaction(r));
                }
            }

            return dto;
        }
    }

    [System.Serializable]
    public class TraitReactionDto
    {
        public string TraitName;
        public int MoodChange;

        public static TraitReactionDto FromReaction(TraitReaction r)
        {
            return new TraitReactionDto
            {
                TraitName = r.Trait != null ? r.Trait.name : null,
                MoodChange = r.MoodChange,
            };
        }
    }
}