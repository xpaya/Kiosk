using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Content.PM;
using Kiosk.Model;

namespace Kiosk.Helper
{
    public class PackageManagerHelper
    {
        public static IList<ResolveInfo> ObtainListOfApplicationIntalled(Context context)
        {
            IList<ResolveInfo> result = null;
            Intent mainIntent = new Intent(Intent.ActionMain, null);
            mainIntent.AddCategory(Intent.CategoryLauncher);
            result = context.PackageManager.QueryIntentActivities(mainIntent, 0);

            return result;
        }

        public static List<AppInfoSelected> GetApplicationsInDevice(Context aplicationContext)
        {

            List<AppInfoSelected> lstDataList = new List<AppInfoSelected>();
            foreach (var appResolve in ObtainListOfApplicationIntalled(aplicationContext))
            {
                string nameApp = aplicationContext.PackageManager.GetApplicationLabel(appResolve.ActivityInfo.ApplicationInfo);
                if (lstDataList.All(x => x.Name != nameApp))
                {
                    AppInfoSelected data = new AppInfoSelected();
                    data.Icon = aplicationContext.PackageManager.GetApplicationIcon(appResolve.ActivityInfo.ApplicationInfo);
                    data.Name = nameApp;
                    data.PackageName = appResolve.ActivityInfo.PackageName;
                    lstDataList.Add(data);
                }
            }
            return lstDataList;
        }

    }
}