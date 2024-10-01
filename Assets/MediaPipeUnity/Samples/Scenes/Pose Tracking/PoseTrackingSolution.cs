// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity.Sample.PoseTracking
{
  public class PoseTrackingSolution : ImageSourceSolution<PoseTrackingGraph>
  {
    [SerializeField] private RectTransform _worldAnnotationArea;
    [SerializeField] private DetectionAnnotationController _poseDetectionAnnotationController;
    [SerializeField] private PoseLandmarkListAnnotationController _poseLandmarksAnnotationController;
    [SerializeField] private PoseWorldLandmarkListAnnotationController _poseWorldLandmarksAnnotationController;
    [SerializeField] private MaskAnnotationController _segmentationMaskAnnotationController;
    [SerializeField] private NormalizedRectAnnotationController _roiFromLandmarksAnnotationController;
    [SerializeField] private ModelRigging _rigging;

    public PoseTrackingGraph.ModelComplexity modelComplexity
    {
      get => graphRunner.modelComplexity;
      set => graphRunner.modelComplexity = value;
    }

    public bool smoothLandmarks
    {
      get => graphRunner.smoothLandmarks;
      set => graphRunner.smoothLandmarks = value;
    }

    public bool enableSegmentation
    {
      get => graphRunner.enableSegmentation;
      set => graphRunner.enableSegmentation = value;
    }

    public bool smoothSegmentation
    {
      get => graphRunner.smoothSegmentation;
      set => graphRunner.smoothSegmentation = value;
    }

    public float minDetectionConfidence
    {
      get => graphRunner.minDetectionConfidence;
      set => graphRunner.minDetectionConfidence = value;
    }

    public float minTrackingConfidence
    {
      get => graphRunner.minTrackingConfidence;
      set => graphRunner.minTrackingConfidence = value;
    }

    protected override void SetupScreen(ImageSource imageSource)
    {
      base.SetupScreen(imageSource);
      _worldAnnotationArea.localEulerAngles = imageSource.rotation.Reverse().GetEulerAngles();
    }

    protected override void OnStartRun()
    {
      if (!runningMode.IsSynchronous())
      {
        graphRunner.OnPoseDetectionOutput += OnPoseDetectionOutput;
        graphRunner.OnPoseLandmarksOutput += OnPoseLandmarksOutput;
        graphRunner.OnPoseWorldLandmarksOutput += OnPoseWorldLandmarksOutput;
        graphRunner.OnSegmentationMaskOutput += OnSegmentationMaskOutput;
        graphRunner.OnRoiFromLandmarksOutput += OnRoiFromLandmarksOutput;
      }

      var imageSource = ImageSourceProvider.ImageSource;
      SetupAnnotationController(_poseDetectionAnnotationController, imageSource);
      SetupAnnotationController(_poseLandmarksAnnotationController, imageSource);
      SetupAnnotationController(_poseWorldLandmarksAnnotationController, imageSource);
      SetupAnnotationController(_segmentationMaskAnnotationController, imageSource);
      _segmentationMaskAnnotationController.InitScreen(imageSource.textureWidth, imageSource.textureHeight);
      SetupAnnotationController(_roiFromLandmarksAnnotationController, imageSource);
    }

    protected override void AddTextureFrameToInputStream(TextureFrame textureFrame)
    {
      graphRunner.AddTextureFrameToInputStream(textureFrame);
    }

    protected override IEnumerator WaitForNextValue()
    {
      var task = graphRunner.WaitNextAsync();
      yield return new WaitUntil(() => task.IsCompleted);

      var result = task.Result;
      // _poseDetectionAnnotationController.DrawNow(result.poseDetection);
      // _poseLandmarksAnnotationController.DrawNow(result.poseLandmarks);
      // _poseWorldLandmarksAnnotationController.DrawNow(result.poseWorldLandmarks);
      // _segmentationMaskAnnotationController.DrawNow(result.segmentationMask);
      // _roiFromLandmarksAnnotationController.DrawNow(result.roiFromLandmarks);

      if (result.poseWorldLandmarks != null)
      {
        IReadOnlyList<Landmark> pac = result.poseWorldLandmarks.Landmark;

        if (pac != null && pac.Count > 0)
        {
          var landmark11 = pac[11];
          var landmark13 = pac[13];
          var landmark15 = pac[15];

          var landmark12 = pac[12];
          var landmark14 = pac[14];
          var landmark16 = pac[16];

          var vec11 = new Vector3(-landmark11.X, -landmark11.Y, landmark11.Z);
          var vec13 = new Vector3(-landmark13.X, -landmark13.Y, landmark13.Z);
          var vec15 = new Vector3(-landmark15.X, -landmark15.Y, landmark15.Z);

          var vec12 = new Vector3(-landmark12.X, -landmark12.Y, landmark12.Z);
          var vec14 = new Vector3(-landmark14.X, -landmark14.Y, landmark14.Z);
          var vec16 = new Vector3(-landmark16.X, -landmark16.Y, landmark16.Z);

          var q1113 = Quaternion.LookRotation(vec11 - vec13);
          var q1315 = Quaternion.LookRotation(vec13 - vec15);
          var q1214 = Quaternion.LookRotation(vec12 - vec14);
          var q1416 = Quaternion.LookRotation(vec14 - vec16);
          _rigging.MapModel(q1113, q1315, q1214, q1416);
        }
        else
        {
          Debug.LogWarning("poseWorldLandmarks contains no landmarks.");
        }
      }
      else
      {
        Debug.LogError("result.poseWorldLandmarks is null.");
      }

      result.segmentationMask?.Dispose();
    }

    private void OnPoseDetectionOutput(object stream, OutputStream<Detection>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get(Detection.Parser);
      _poseDetectionAnnotationController.DrawLater(value);
    }

    private void OnPoseLandmarksOutput(object stream, OutputStream<NormalizedLandmarkList>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get(NormalizedLandmarkList.Parser);
      _poseLandmarksAnnotationController.DrawLater(value);
    }

    private void OnPoseWorldLandmarksOutput(object stream, OutputStream<LandmarkList>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get(LandmarkList.Parser);
      _poseWorldLandmarksAnnotationController.DrawLater(value);
    }

    private void OnSegmentationMaskOutput(object stream, OutputStream<ImageFrame>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get();
      _segmentationMaskAnnotationController.DrawLater(value);
      value?.Dispose();
    }

    private void OnRoiFromLandmarksOutput(object stream, OutputStream<NormalizedRect>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get(NormalizedRect.Parser);
      _roiFromLandmarksAnnotationController.DrawLater(value);
    }
  }
}
