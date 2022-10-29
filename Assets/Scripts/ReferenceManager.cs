using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{

    public class ReferenceManager : MonoBehaviour
    {
        #region Instance

        public static ReferenceManager Instance;

        #endregion

        #region Fields

        [Header("Components")]
        #region Private Fields

        private GameObject _centerHexagon;

        #endregion

        #region Properties

        public GameObject CenterHexagon { get { return _centerHexagon; } set { _centerHexagon = value; } }

        #endregion

        #region Awake and Start

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            _centerHexagon = GameObject.Find("Center Hexagon");
        }

        #endregion

        #endregion
    }

}

