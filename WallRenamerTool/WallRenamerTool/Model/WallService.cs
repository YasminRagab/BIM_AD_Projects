using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace WallRenamerTool.Model
{
    public static class WallService
    {
        public static List<Wall> GetWallsAboveHeight(double height)
        {
            Document doc = Singleton.Instance.Doc;
            return new FilteredElementCollector(doc)
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .Where(w =>
                {
                    Parameter p = w.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
                    return p != null && p.AsDouble() > height;
                })
                .ToList();
        }

        public static int RenameWalls(List<Wall> walls, string prefix)
        {
            Document doc = Singleton.Instance.Doc;
            int count = 0;

            using (Transaction t = new Transaction(doc, "Rename Walls"))
            {
                t.Start();
                foreach (var wall in walls)
                {
                    Parameter nameParam = wall.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
                    if (nameParam != null && nameParam.HasValue)
                    {
                        nameParam.Set(prefix + nameParam.AsString());
                        count++;
                    }
                }
                t.Commit();
            }

            return count;
        }
    }
}