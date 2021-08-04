using RTD.Hexagons;
using RTD.Hexgrid;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RTD.HexgridEditing {
    [InitializeOnLoad]
    public class HexBrushPalette : EditorWindow {

        const float ICON_SIZE = 60;
        const float SPACE_SIZE = 10;
        const int GIZMO_RADIUS = 22;

        public static HexBrushPalette instance = default;
        public HexBrush[] brushes = new HexBrush[0];
        public HexTile[] tiles = new HexTile[0];

        public bool isDrawing = false;

        public HexMap currentMap = default;
        public HexBrush currentBrush = default;
        public HexTile currentTile = default;
        public Hex3 currentHex = default;

        int brushSelect = 0;
        int tileSelect = 0;
        int gridColumns => (int) (position.width / ICON_SIZE);

        float gridHeight => tiles.Length / gridColumns * ICON_SIZE;

        private void OnSelectionChange() {
            if (Selection.activeGameObject && Selection.activeGameObject.TryGetComponent(out HexMap map)) {
                currentMap = map;
            }
        }

        private void OnGUI() {
            if (currentMap) {
                GUILayout.Label(currentMap.name);
            } else {
                GUILayout.Label("Click on map to start Editing");
            }
            GUILayout.Space(SPACE_SIZE);
            GUILayout.Label("Select Brush");
            brushSelect = GUILayout.Toolbar(brushSelect, brushes.Select(bru => bru.GetPreviewTexture()).ToArray(), GUILayout.Height(ICON_SIZE));
            currentBrush = brushes[brushSelect];
            GUILayout.Space(SPACE_SIZE);
            GUILayout.Label("Select Tile");
            tileSelect = GUILayout.SelectionGrid(tileSelect, tiles.Select(tile => tile.GetPreviewTexture()).ToArray(), gridColumns, GUILayout.Height(gridHeight));
            currentTile = tiles[tileSelect];
            GUILayout.Space(SPACE_SIZE);
            if (GUILayout.Button("Refresh Tiles")){
                Refresh();
            }
            if (currentMap) {
                if (GUILayout.Button("ClearMap")) {
                    currentMap.Clear();
                }
            }
        }


        void OnSceneGUI(SceneView sceneView) {
            if (!currentMap) {
                return;
            }
            var mousePos = Event.current.mousePosition;
            mousePos.y = sceneView.camera.pixelHeight - mousePos.y;
            var ray = sceneView.camera.ScreenPointToRay(mousePos);
            currentMap.Raycast(ray, out currentHex);

            if (!currentBrush || !currentTile) {
                return;
            }
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            var e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0) {
                currentBrush.StartDraw(currentTile, currentMap, currentHex);
                isDrawing = true;
                e.Use();
            }
            if (e.type == EventType.MouseDrag && e.button == 0) {
                currentBrush.DragDraw(currentTile, currentMap, currentHex);
                e.Use();
            }
            if (e.type == EventType.MouseUp && e.button == 0) {
                currentBrush.EndDraw(currentTile, currentMap, currentHex);
                isDrawing = false;
                e.Use();
            }
        }

        void Refresh() {
            brushes = Resources.LoadAll<HexBrush>("");
            tiles = Resources.LoadAll<HexTile>("");
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.NotInSelectionHierarchy)]
        static void DrawCurrentHex(HexMap hexagonMap, GizmoType gizmoType) {
            if (instance && hexagonMap == instance.currentMap) {
                for(int i=1; i<GIZMO_RADIUS; i++) {
                    Gizmos.color = Color.Lerp(Color.gray, Color.clear, (float)i / GIZMO_RADIUS);
                    foreach(var hex in HexUtility.HexRing(i, instance.currentHex)) {
                        hexagonMap.DrawHexOutline(hex);
                    }
                }
                Gizmos.color = Color.yellow;
                hexagonMap.DrawHexOutline(instance.currentHex);
                if (instance.currentBrush && instance.currentTile  && instance.isDrawing) {
                    Gizmos.color = instance.currentTile.GetTelegraphColor();
                    foreach(var hex in instance.currentBrush.GetHexTelegraph(instance.currentHex)) {
                        hexagonMap.DrawHexOutline(hex);
                    }
                }
            }
        }

        [MenuItem("Tools/GameObject Brush")]
        public static void ShowWindow() {
            DontDestroyOnLoad(GetWindow<HexBrushPalette>("Hex Palette"));
        }

        private void OnEnable() {
            if (Selection.activeGameObject) {
                Selection.activeGameObject.TryGetComponent(out currentMap);
            }
            instance = this;
            SceneView.duringSceneGui += OnSceneGUI;
            Refresh();
        }

        private void OnDisable() {
            instance = null;
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }
}
