using UnityEngine;
using Zenject;
using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Version
{
    [CreateAssetMenu(fileName = "VersionSettingsInstaller", menuName = "Installers/VersionSettingsInstaller")]
    public class VersionSettingsInstaller : ScriptableObjectInstaller<VersionSettingsInstaller>
    {
        [SerializeField]
        private IPhotos gallery;
        [SerializeField]
        private IWordlist wordlist;

        public override void InstallBindings()
        {
            Container.BindInstance(gallery).AsSingle();
            Container.BindInstance(wordlist).AsSingle();
        }
    }
}