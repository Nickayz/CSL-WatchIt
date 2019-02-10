﻿using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WatchIt
{
    public class Watcher : MonoBehaviour
    {
        private bool _initialized;
        private float _timer;

        private UIButton _esc;
        private UITextureAtlas _textureAtlas;
        private UIPanel _panel;
        private UIDragHandle _dragHandle;
        private UISprite _dragSprite;
        private UISprite _orientationSprite;
        private UIButton _limitsButton;
        private UIButton _statisticsButton;

        private List<Watch> _watches;

        private void Awake()
        {
            try
            {
                if (_esc == null)
                {
                    _esc = GameObject.Find("Esc").GetComponent<UIButton>();
                }

                if (ModConfig.Instance.PositionX == 0.0f)
                {
                    ModConfig.Instance.PositionX = _esc.absolutePosition.x + 13f;
                }

                if (ModConfig.Instance.PositionY == 0.0f)
                {
                    ModConfig.Instance.PositionY = _esc.absolutePosition.y + 50f;
                }

                _textureAtlas = LoadResources();

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:Awake -> Exception: " + e.Message);
            }
        }

        private void OnEnable()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:OnEnable -> Exception: " + e.Message);
            }
        }

        private void Start()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:Start -> Exception: " + e.Message);
            }
        }

        private void Update()
        {
            try
            {
                if (!_initialized || ModConfig.Instance.ConfigUpdated)
                {
                    UpdateUI();

                    _initialized = true;
                    ModConfig.Instance.ConfigUpdated = false;
                }
                else
                {
                    _timer += Time.deltaTime;

                    if (_timer > ModConfig.Instance.RefreshInterval)
                    {
                        _timer = _timer - ModConfig.Instance.RefreshInterval;

                        UpdateWatches();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:Update -> Exception: " + e.Message);
            }
        }

        private void OnDisable()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:OnDisable -> Exception: " + e.Message);
            }
        }

        private void OnDestroy()
        {
            try
            {
                if (_panel != null)
                {
                    Destroy(_panel);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:OnDestroy -> Exception: " + e.Message);
            }
        }

        private UITextureAtlas LoadResources()
        {
            try
            {
                if (_textureAtlas == null)
                {
                    string[] spriteNames = new string[]
                    {
                        "CircleNormal",
                        "CircleHovered",
                        "CirclePressed",
                        "RectNormal",
                        "RectHovered",
                        "RectPressed",
                        "Drag",
                        "Horizontal",
                        "Vertical",
                        "DragHover",
                        "HorizontalHover",
                        "VerticalHover",
                        "WatchGreen",
                        "WatchYellow",
                        "WatchRed",
                        "Electricity",
                        "Water",
                        "Sewage",
                        "Garbage",
                        "ElementarySchool",
                        "HighSchool",
                        "University",
                        "Healthcare",
                        "Crematorium",
                        "FireDepartment",
                        "PoliceDepartment",
                        "Jail",
                        "Heating",
                        "Landfill",
                        "Health",
                        "Cemetery",
                        "Traffic",
                        "Fire",
                        "Crime",
                        "Unemployment",
                        "Statistics",
                        "Limits"
                    };

                    _textureAtlas = ResourceLoader.CreateTextureAtlas("WatchItAtlas", spriteNames, "WatchIt.Icons.");
                }

                return _textureAtlas;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:LoadResources -> Exception: " + e.Message);
                return null;
            }
        }

        private void CreateUI()
        {
            try
            {
                _panel = UIUtils.CreatePanel("WatchIt");
                _panel.autoSize = false;
                _panel.autoLayout = false;

                _panel.absolutePosition = new Vector3(ModConfig.Instance.PositionX, ModConfig.Instance.PositionY);

                _dragHandle = UIUtils.CreateDragHandle(_panel);
                _dragHandle.tooltip = "Drag to move panel";
                _dragHandle.size = new Vector2(15f, 15f);
                _dragHandle.relativePosition = new Vector3(3f, 3f);
                _dragHandle.eventMouseEnter += (component, eventParam) =>
                {
                    _dragSprite.spriteName = "DragHover";
                };
                _dragHandle.eventMouseLeave += (component, eventParam) =>
                {
                    _dragSprite.spriteName = "Drag";
                };
                _dragHandle.eventMouseUp += (component, eventParam) =>
                {
                    ModConfig.Instance.PositionX = _panel.absolutePosition.x;
                    ModConfig.Instance.PositionY = _panel.absolutePosition.y;
                    ModConfig.Instance.Save();
                };

                _dragSprite = UIUtils.CreateSprite(_panel, "Drag", _textureAtlas, "Drag");
                _dragSprite.isInteractive = false;
                _dragSprite.size = new Vector2(15f, 15f);
                _dragSprite.relativePosition = new Vector3(3f, 3f);

                _orientationSprite = UIUtils.CreateSprite(_panel, "Orientation", _textureAtlas, "Vertical");
                _orientationSprite.size = new Vector2(15f, 15f);
                _orientationSprite.eventClick += (component, eventParam) =>
                {
                    ModConfig.Instance.VerticalLayout = !ModConfig.Instance.VerticalLayout;
                    ModConfig.Instance.Save();
                    UpdateUI();
                };

                UpdateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                if (ModConfig.Instance.VerticalLayout)
                {
                    _panel.width = 36f;                   

                    _orientationSprite.tooltip = "Click to layout panel horizontally";
                    _orientationSprite.spriteName = "Horizontal";
                    _orientationSprite.relativePosition = new Vector3(18f, 3f);
                    _orientationSprite.eventMouseEnter += (component, eventParam) =>
                    {
                        _orientationSprite.spriteName = "HorizontalHover";
                    };
                    _orientationSprite.eventMouseLeave += (component, eventParam) =>
                    {
                        _orientationSprite.spriteName = "Horizontal";
                    };
                }
                else
                {
                    _panel.height = 36f;

                    _orientationSprite.tooltip = "Click to layout panel vertically";
                    _orientationSprite.spriteName = "Vertical";
                    _orientationSprite.relativePosition = new Vector3(3f, 18f);
                    _orientationSprite.eventMouseEnter += (component, eventParam) =>
                    {
                        _orientationSprite.spriteName = "VerticalHover";
                    };
                    _orientationSprite.eventMouseLeave += (component, eventParam) =>
                    {
                        _orientationSprite.spriteName = "Vertical";
                    };
                }

                CreateOrUpdatePanelButtons();

                UpdateWatches();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateOrUpdatePanelButtons()
        {
            try
            {
                if (_watches == null)
                {
                    _watches = new List<Watch>();
                }
                else
                {
                    foreach (Watch watch in _watches)
                    {
                        watch.DestroyWatch();
                    }

                    _watches.Clear();

                    Destroy(_limitsButton);
                    Destroy(_statisticsButton);
                }

                if (ModConfig.Instance.ElectricityAvailability)
                {
                    _watches.Add(CreateWatch("Electricity", Watch.WatchType.Aspect, "Electricity", "Electricity Availability"));
                }
                if (ModConfig.Instance.WaterAvailability)
                {
                    _watches.Add(CreateWatch("Water", Watch.WatchType.Aspect, "Water", "Water Availability"));
                }
                if (ModConfig.Instance.SewageAvailability)
                {
                    _watches.Add(CreateWatch("Sewage", Watch.WatchType.Aspect, "Sewage", "Sewage Availability"));
                }
                if (ModConfig.Instance.GarbageAvailability)
                {
                    _watches.Add(CreateWatch("Garbage", Watch.WatchType.Aspect, "Garbage", "Garbage Availability"));
                }
                if (ModConfig.Instance.ElementarySchoolAvailability)
                {
                    _watches.Add(CreateWatch("ElementarySchool", Watch.WatchType.Aspect, "ElementarySchool", "Elementary School Availability"));
                }
                if (ModConfig.Instance.HighSchoolAvailability)
                {
                    _watches.Add(CreateWatch("HighSchool", Watch.WatchType.Aspect, "HighSchool", "High School Availability"));
                }
                if (ModConfig.Instance.UniversityAvailability)
                {
                    _watches.Add(CreateWatch("University", Watch.WatchType.Aspect, "University", "University Availability"));
                }
                if (ModConfig.Instance.HealthcareAvailability)
                {
                    _watches.Add(CreateWatch("Healthcare", Watch.WatchType.Aspect, "Healthcare", "Healthcare Availability"));
                }
                if (ModConfig.Instance.CrematoriumAvailability)
                {
                    _watches.Add(CreateWatch("Crematorium", Watch.WatchType.Aspect, "Crematorium", "Crematorium Availability"));
                }
                if (ModConfig.Instance.FireDepartmentAvailability)
                {
                    _watches.Add(CreateWatch("FireDepartment", Watch.WatchType.Aspect, "FireDepartment", "Fire Department Availability"));
                }
                if (ModConfig.Instance.PoliceDepartmentAvailability)
                {
                    _watches.Add(CreateWatch("PoliceDepartment", Watch.WatchType.Aspect, "PoliceDepartment", "Police Department Availability"));
                }
                if (ModConfig.Instance.JailAvailability)
                {
                    _watches.Add(CreateWatch("Jail", Watch.WatchType.Aspect, "Jail", "Jail Availability"));
                }
                if (ModConfig.Instance.HeatingAvailability)
                {
                    _watches.Add(CreateWatch("Heating", Watch.WatchType.Aspect, "Heating", "Heating Availability"));
                }
                if (ModConfig.Instance.LandfillUsage)
                {
                    _watches.Add(CreateWatch("Landfill", Watch.WatchType.Pillar, "Landfill", "Landfill Usage"));
                }
                if (ModConfig.Instance.CemeteryUsage)
                {
                    _watches.Add(CreateWatch("Cemetery", Watch.WatchType.Pillar, "Cemetery", "Cemetery Usage"));
                }
                if (ModConfig.Instance.HealthAverage)
                {
                    _watches.Add(CreateWatch("Health", Watch.WatchType.Pillar, "Health", "Health Average"));
                }
                if (ModConfig.Instance.TrafficFlow)
                {
                    _watches.Add(CreateWatch("Traffic", Watch.WatchType.Pillar, "Traffic", "Traffic Flow"));
                }
                if (ModConfig.Instance.FireHazard)
                {
                    _watches.Add(CreateWatch("Fire", Watch.WatchType.Pillar, "Fire", "Fire Hazard"));
                }
                if (ModConfig.Instance.CrimeRate)
                {
                    _watches.Add(CreateWatch("Crime", Watch.WatchType.Pillar, "Crime", "Crime Rate"));
                }
                if (ModConfig.Instance.UnemploymentRate)
                {
                    _watches.Add(CreateWatch("Unemployment", Watch.WatchType.Pillar, "Unemployment", "Unemployment Rate"));
                }

                int buttonIndex = _watches.Count;

                if (ModConfig.Instance.ShowGameLimitsButton)
                {
                    buttonIndex++;

                    _limitsButton = UIUtils.CreateButton(_panel, "Limits", _textureAtlas, "Circle");
                    _limitsButton.tooltip = "Game Limits";
                    _limitsButton.size = new Vector2(33f, 33f);
                    _limitsButton.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(0f, 36f * buttonIndex + 22f) : new Vector3(36f * buttonIndex + 22f, 0f);

                    _limitsButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
                    _limitsButton.normalFgSprite = "Limits";
                    _limitsButton.hoveredFgSprite = "Limits";
                    _limitsButton.pressedFgSprite = "Limits";
                    _limitsButton.disabledFgSprite = "Limits";

                    _limitsButton.eventClicked += (component, eventParam) =>
                    {
                        LimitsPanel limitsPanel = GameObject.Find("WatchItLimitsPanel").GetComponent<LimitsPanel>();

                        if (limitsPanel != null)
                        {
                            if (limitsPanel.isVisible)
                            {
                                limitsPanel.Hide();
                            }
                            else
                            {
                                limitsPanel.Show();
                            }
                        }
                    };
                }

                if (ModConfig.Instance.ShowCityStatisticsButton)
                {
                    buttonIndex++;

                    _statisticsButton = UIUtils.CreateButton(_panel, "Statistics", _textureAtlas, "Circle");
                    _statisticsButton.tooltip = "City Statistics";
                    _statisticsButton.size = new Vector2(33f, 33f);
                    _statisticsButton.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(0f, 36f * buttonIndex + 22f) : new Vector3(36f * buttonIndex + 22f, 0f);

                    _statisticsButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
                    _statisticsButton.normalFgSprite = "Statistics";
                    _statisticsButton.hoveredFgSprite = "Statistics";
                    _statisticsButton.pressedFgSprite = "Statistics";
                    _limitsButton.disabledFgSprite = "Statistics";

                    _statisticsButton.eventClicked += (component, eventParam) =>
                    {
                        UIView.library.ShowModal("StatisticsPanel");
                    };
                }

                if (ModConfig.Instance.VerticalLayout)
                {
                    _panel.height = 36f * ++buttonIndex + 22f;
                }
                else
                {
                    _panel.width = 36f * ++buttonIndex + 22f;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:CreateOrUpdatePanelButtons -> Exception: " + e.Message);
            }
        }

        private Watch CreateWatch(string name, Watch.WatchType type, string spriteName, string toolTip)
        {
            Watch watch = new Watch();

            try
            {
                watch.CreateWatch(_panel, name, type, ModConfig.Instance.VerticalLayout, _watches.Count, _textureAtlas, spriteName, toolTip);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:CreateWatch -> Exception: " + e.Message);
            }

            return watch;
        }

        private void UpdateWatches()
        {
            try
            {
                foreach (Watch watch in _watches)
                {
                    watch.UpdateWatch();
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:UpdateWatches -> Exception: " + e.Message);
            }
        }
    }
}