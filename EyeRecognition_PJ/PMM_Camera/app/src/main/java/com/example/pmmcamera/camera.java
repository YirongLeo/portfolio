package com.example.pmmcamera;
import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.content.ContextCompat;

import android.Manifest;
import android.content.Context;
import android.content.DialogInterface;
import android.content.pm.PackageManager;
import android.graphics.Camera;
import android.graphics.SurfaceTexture;
import android.hardware.camera2.CameraAccessException;
import android.hardware.camera2.CameraCaptureSession;
import android.hardware.camera2.CameraCharacteristics;
import android.hardware.camera2.CameraDevice;
import android.hardware.camera2.CameraManager;
import android.hardware.camera2.CaptureRequest;
import android.hardware.camera2.params.StreamConfigurationMap;
import android.media.MediaCodec;
import android.media.MediaRecorder;
import android.nfc.Tag;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.os.HandlerThread;
import android.os.SystemClock;
import android.text.InputType;
import android.util.Log;
import android.util.Size;
import android.util.SparseIntArray;
import android.view.Surface;
import android.view.TextureView;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.Parameter;
import java.sql.Time;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;
import java.util.Date;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;

public class camera extends AppCompatActivity
{

    private static final int REQUEST_CAMERA_PERMISSION_RESULT = 0;
    private static final int REQUEST_WRITE_EXTERNAL_STORAGE_PERMISSION_RESULT = 1;
    private TextureView mTextureView;

    //抓取相機內的畫面
    private TextureView.SurfaceTextureListener mSurfaceTextureListener = new TextureView.SurfaceTextureListener()
    {
        @Override
        public void onSurfaceTextureAvailable(@NonNull SurfaceTexture surface, int width, int height)
        {
            setupCamera(width, height);
            connectCamera();

        }

        @Override
        public void onSurfaceTextureSizeChanged(@NonNull SurfaceTexture surface, int width, int height)
        {

        }

        @Override
        public boolean onSurfaceTextureDestroyed(@NonNull SurfaceTexture surface)
        {
            return false;
        }

        @Override
        public void onSurfaceTextureUpdated(@NonNull SurfaceTexture surface)
        {

        }
    };

    //設定CameraDevice
    private CameraDevice mCameraDevice;
    private CameraDevice.StateCallback mCameraDeviceStateCallback = new CameraDevice.StateCallback() {
        @RequiresApi(api = Build.VERSION_CODES.M)
        @Override
        public void onOpened(@NonNull CameraDevice camera) {
            mCameraDevice = camera;
            if (mIsRecording)
            {
                try
                {
                    createVideoFileName();

                } catch (IOException e)
                {
                    e.printStackTrace();
                }
                mMediaRecorder.start();

            }
            else
            {
                startPreview();
                Toast.makeText(getApplicationContext(), "相機連結成功", Toast.LENGTH_SHORT).show();
            }
        }

        @Override
        public void onDisconnected(@NonNull CameraDevice camera)
        {
            camera.close();
            mCameraDevice = null;
        }

        @Override
        public void onError(@NonNull CameraDevice camera, int error)
        {
            camera.close();
            mCameraDevice = null;
        }
    };


    private HandlerThread mBackgroundHandlerThread;
    private Handler mBackgroundHandler;
    private String mCameraId;
    private Size mPreviewSize;
    private Size mVideoSize;
    private MediaRecorder mMediaRecorder;
    private int mTotalRotation;
    private CaptureRequest.Builder mCaptureRequestBuilder = null;
    private ImageView mRecordImageButton,muser;
    private boolean mIsRecording = false;
    private File mVideoFolder;
    private String mVideoFileName;
    private static
    SparseIntArray ORIENTATIONS = new SparseIntArray();
    Surface previewSurface;
    Surface recorderSurface;
    long Last_Opreation_Time = 0;
    private int eyecount = 0;
    Handler handler = new Handler();
    private String myText;


    //設定手機旋轉的方向
    static
    {
        ORIENTATIONS.append(Surface.ROTATION_0, 0);
        ORIENTATIONS.append(Surface.ROTATION_90, 90);
        ORIENTATIONS.append(Surface.ROTATION_180, 180);
        ORIENTATIONS.append(Surface.ROTATION_270, 270);
    }

    private static class CompareSizeByArea implements Comparator<Size>
    {

        @Override
        public int compare(Size lhs, Size rhs)
        {
            return Long.signum((long) lhs.getWidth() * lhs.getHeight() / (long) rhs.getWidth() * rhs.getHeight());
        }
    }



    //主程式
    @RequiresApi(api = Build.VERSION_CODES.M)
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_camera);

        createVideoFolder();

        mMediaRecorder = new MediaRecorder();
        mTextureView = (TextureView) findViewById(R.id.textureView);
        mRecordImageButton = (ImageView) findViewById(R.id.videoOnlineImageButton);
        muser = (ImageView) findViewById(R.id.user);

        //設定按下錄影按鈕的事件
        mRecordImageButton.setOnClickListener(new View.OnClickListener()
        {

            @RequiresApi(api = Build.VERSION_CODES.M)
            @Override
            public void onClick(View v)
            {

                //設定當下系統的秒數
                long current = java.lang.System.currentTimeMillis();
                //判斷如果小於4秒，不執行任何動作
                if (current - Last_Opreation_Time < 4000)
                {
                    return;
                }
                Last_Opreation_Time = current;

                checkWriteStoragePermission();

                new Thread(new Runnable()
                {
                    @Override
                    public void run()
                    {

                        try
                        {
                            Thread.sleep(2000);
                            runOnUiThread(new Runnable()
                            {
                                @RequiresApi(api = Build.VERSION_CODES.N)
                                @Override
                                public void run()
                                {
                                    //停止錄影
                                    mMediaRecorder.stop();
                                    //釋放畫面
                                    mCaptureRequestBuilder.removeTarget(recorderSurface);

                                }
                            });
                        }

                        catch (InterruptedException e)
                        {
                            e.printStackTrace();
                        }

                    }
                }).start();

                new Thread(new Runnable()
                {
                    @Override
                    public void run()
                    {

                        try
                        {
                            Thread.sleep(2300);
                        }

                        catch (InterruptedException e)
                        {
                            e.printStackTrace();
                        }

                        runOnUiThread(new Runnable()
                        {
                            @RequiresApi(api = Build.VERSION_CODES.N)
                            @Override
                            public void run()
                            {

                                try
                                {
                                    createVideoFileName();
                                    setupMediaRecorder();
                                }

                                catch (IOException e)
                                {
                                    e.printStackTrace();
                                }

                                //開始錄影
                                mMediaRecorder.start();
                                mCaptureRequestBuilder.set(CaptureRequest.FLASH_MODE, CaptureRequest.CONTROL_AE_MODE_OFF);
                                mCaptureRequestBuilder.set(CaptureRequest.FLASH_MODE, CaptureRequest.FLASH_MODE_TORCH);
                            }
                        });

                    }
                }).start();

                new Thread(new Runnable()
                {
                    @Override
                    public void run()
                    {

                        try
                        {
                            Thread.sleep(3500);
                        }

                        catch (InterruptedException e)
                        {
                            e.printStackTrace();
                        }

                        runOnUiThread(new Runnable()
                        {
                            @RequiresApi(api = Build.VERSION_CODES.N)
                            @Override
                            public void run()
                            {
                                mCaptureRequestBuilder.set(CaptureRequest.FLASH_MODE, CaptureRequest.FLASH_MODE_OFF);
                                //停止錄影
                                mMediaRecorder.stop();
                                //釋放畫面
                                mCaptureRequestBuilder.removeTarget(recorderSurface);
                            }
                        });
                    }
                }).start();

                new Thread(new Runnable()
                {
                    @Override
                    public void run()
                    {

                        try
                        {
                            Thread.sleep(4000);
                        }

                        catch (InterruptedException e)
                        {
                            e.printStackTrace();
                        }
                        runOnUiThread(new Runnable()
                        {
                            @RequiresApi(api = Build.VERSION_CODES.N)
                            @Override
                            public void run()
                            {
                                try
                                {
                                    createVideoFileName();
                                    setupMediaRecorder();
                                }

                                catch (IOException e)
                                {
                                    e.printStackTrace();
                                }
                                //開始錄影
                                mMediaRecorder.start();
                            }
                        });

                    }
                }).start();

                new Thread(new Runnable()
                {
                    @Override
                    public void run()
                    {

                        try
                        {
                            Thread.sleep(5000);
                        }

                        catch (InterruptedException e)
                        {
                            e.printStackTrace();
                        }
                        runOnUiThread(new Runnable()
                        {
                            @RequiresApi(api = Build.VERSION_CODES.N)
                            @Override
                            public void run()
                            {
                                //變換錄影時的Icon
                                mRecordImageButton.setImageResource(R.drawable.recording);
                                //停止錄影
                                mMediaRecorder.stop();
                                //釋放畫面
                                mCaptureRequestBuilder.removeTarget(recorderSurface);
                                startPreview();
                            }
                        });
                    }
                }).start();
            }

        });

        muser.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                //設定AlertDialog
                AlertDialog.Builder mydialog = new AlertDialog.Builder(camera.this);
                //設定對話框出現的文字
                mydialog.setTitle("請輸入使用者的名稱");

                final EditText weightInput = new EditText(camera.this);
                //將型態設定為可以輸入文字
                weightInput.setInputType(InputType.TYPE_CLASS_TEXT);
                mydialog.setView(weightInput);

                mydialog.setPositiveButton("OK", new DialogInterface.OnClickListener()
                {
                    @Override
                    public void onClick(DialogInterface dialog, int which)
                    {
                        myText = weightInput.getText().toString();
                        Toast.makeText(camera.this,"影片的名稱設定為" + myText, Toast.LENGTH_LONG).show();
                    }
                });

                mydialog.setNegativeButton("Cancel", new DialogInterface.OnClickListener()
                {
                    @Override
                    public void onClick(DialogInterface dialogInter, int which)
                    {
                        dialogInter.cancel();
                    }
                });
                mydialog.show();
            }
        });


    }


    @Override
    protected void onResume()
    {
        super.onResume();

        //先呼叫後台Thread
        startBackgroundThread();
        //抓取mTextureView的寬度跟高度
        if (mTextureView.isAvailable())
        {
            setupCamera(mTextureView.getWidth(), mTextureView.getHeight());
            connectCamera();
        } else
        {
            mTextureView.setSurfaceTextureListener(mSurfaceTextureListener);
        }
    }

    //檢查手機是否有鏡頭
    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults)
    {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        if (requestCode == REQUEST_CAMERA_PERMISSION_RESULT)
        {
            if (grantResults[0] != PackageManager.PERMISSION_GRANTED)
            {
                Toast.makeText(getApplicationContext(), "並沒有在您的手機上偵測到鏡頭", Toast.LENGTH_SHORT).show();
            }
        }

        if (requestCode == REQUEST_WRITE_EXTERNAL_STORAGE_PERMISSION_RESULT)
        {
            if (grantResults[0] == PackageManager.PERMISSION_GRANTED)
            {
                mIsRecording = true;
                //變換錄影時的Icon
                mRecordImageButton.setImageResource(R.drawable.record);
                try
                {
                    createVideoFileName();
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                }
                Toast.makeText(this, "請求權限成功 ", Toast.LENGTH_SHORT).show();
            } else {
                Toast.makeText(this, "App需要儲存影片的權限才能執行", Toast.LENGTH_SHORT).show();
            }
        }
    }

    @Override
    protected void onPause()
    {
        //相機暫停時釋放資源
        closeCamera();
        //停止後台Thread
        stopBackgroundThread();
        super.onPause();
    }

    //隱藏上方及下方的UI
    @Override
    public void onWindowFocusChanged(boolean hasFocus)
    {
        super.onWindowFocusChanged(hasFocus);
        View decorView = getWindow().getDecorView();
        if (hasFocus)
        {
            decorView.setSystemUiVisibility(View.SYSTEM_UI_FLAG_LAYOUT_STABLE
                    | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY
                    | View.SYSTEM_UI_FLAG_FULLSCREEN
                    | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                    | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                    | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION);
        }
    }

    //設定相機
    private void setupCamera(int width, int height)
    {
        CameraManager cameraManager = (CameraManager) getSystemService(Context.CAMERA_SERVICE);
        try {
            //抓取從相機的表單中抓取每個相機
            for (String cameraId : cameraManager.getCameraIdList())
            {
                //選擇適合的相機
                CameraCharacteristics cameraCharacteristics = cameraManager.getCameraCharacteristics(cameraId);
                //檢查鏡頭是否為前面，是的話就跳過
                if (cameraCharacteristics.get(CameraCharacteristics.LENS_FACING) ==
                        CameraCharacteristics.LENS_FACING_FRONT)
                {
                    continue;
                }

                StreamConfigurationMap map = cameraCharacteristics.get(CameraCharacteristics.SCALER_STREAM_CONFIGURATION_MAP);
                //從Manager中獲得旋轉值
                int deviceOrientation = getWindowManager().getDefaultDisplay().getRotation();
                //最終得到的旋轉值
                mTotalRotation = sensorToDeviceRotation(cameraCharacteristics, deviceOrientation);
                boolean swapRotation = mTotalRotation == 90 || mTotalRotation == 270;
                int rotatedWidth = width;
                int rotatedHeight = height;
                if (swapRotation)
                {
                    rotatedWidth = height;
                    rotatedHeight = width;
                }
                //顯示設定好的preview
                mPreviewSize = chooseOptimalSize(map.getOutputSizes(SurfaceTexture.class), rotatedWidth, rotatedHeight);
                //顯示設定好的video
                mVideoSize = chooseOptimalSize(map.getOutputSizes(MediaRecorder.class), rotatedWidth, rotatedHeight);
                mCameraId = cameraId;
                return;
            }
        }
        catch (CameraAccessException e)
        {
            e.printStackTrace();
        }
    }

    //連結相機
    private void connectCamera()
    {
        //從CameraService裡面獲取權限
        CameraManager cameraManager = (CameraManager) getSystemService(Context.CAMERA_SERVICE);
        try {
            //檢查相機的SDK是否符合Camera的權限
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M)
            {
                if (ContextCompat.checkSelfPermission(this, Manifest.permission.CAMERA) == PackageManager.PERMISSION_GRANTED)
                {
                    cameraManager.openCamera(mCameraId, mCameraDeviceStateCallback, mBackgroundHandler);
                }
                else
                {
                    if (shouldShowRequestPermissionRationale(Manifest.permission.CAMERA))
                    {
                        Toast.makeText(this, "要求進入相機", Toast.LENGTH_SHORT).show();
                    }
                    requestPermissions(new String[]{Manifest.permission.CAMERA}, REQUEST_CAMERA_PERMISSION_RESULT);
                }
            }
            else
            {
                cameraManager.openCamera(mCameraId, mCameraDeviceStateCallback, mBackgroundHandler);
            }
        }
        catch (CameraAccessException e)
        {
            e.printStackTrace();
        }
    }

    //顯示在手機上的畫面
    void Rebuild_Camera_Capture(List<Surface> surfaces)
    {
        try {
            mCameraDevice.createCaptureSession(surfaces, new CameraCaptureSession.StateCallback()
            {
                //更新畫面
                @Override
                public void onConfigured(@NonNull CameraCaptureSession session)
                {
                    try
                    {
                        session.setRepeatingRequest(mCaptureRequestBuilder.build(), null, mBackgroundHandler);
                    }
                    catch (CameraAccessException  e)
                    {
                        e.printStackTrace();
                    }
                }

                @Override
                public void onConfigureFailed(@NonNull CameraCaptureSession session)
                {
                    Toast.makeText(getApplicationContext(), "畫面讀取失敗", Toast.LENGTH_SHORT).show();
                }
            }, null);
        }
        catch (CameraAccessException e)
        {
            e.printStackTrace();
        }
    }

    //啟用相機的畫面
    @RequiresApi(api = Build.VERSION_CODES.M)
    private void startPreview()
    {
        try
        {
            //設定第一次執行的畫面
            if(mCaptureRequestBuilder == null)
            {
                SurfaceTexture surfaceTexture = mTextureView.getSurfaceTexture();
                //設定畫面Size
                surfaceTexture.setDefaultBufferSize(mPreviewSize.getWidth(), mPreviewSize.getHeight());
                //設定新的preview
                previewSurface = new Surface(surfaceTexture);
                //請求預覽相機畫面
                mCaptureRequestBuilder = mCameraDevice.createCaptureRequest(CameraDevice.TEMPLATE_RECORD);
                //獲得preview的畫面
                mCaptureRequestBuilder.addTarget(previewSurface);
            }

            //輸出給手機的畫面
            Rebuild_Camera_Capture(Arrays.asList(previewSurface));
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }

    }

    //可以任意使用的釋放資源
    private void closeCamera()
    {
        if (mCameraDevice != null)
        {
            mCameraDevice.close();
            mCameraDevice = null;
        }
    }

    //設定後台Thread的啟動，主要是用來傳遞相機的API
    private void startBackgroundThread()
    {
        mBackgroundHandlerThread = new HandlerThread("Camera2VideImage");
        mBackgroundHandlerThread.start();
        mBackgroundHandler = new Handler(mBackgroundHandlerThread.getLooper());
    }

    //設定後台Thread的停止
    private void stopBackgroundThread()
    {
        mBackgroundHandlerThread.quitSafely();
        try
        {
            //.join()的執行緒會等到上一個相同的Thread結束後,才能繼續執行
            mBackgroundHandlerThread.join();
            mBackgroundHandlerThread = null;
            mBackgroundHandler = null;
        } catch (InterruptedException e)
        {
            e.printStackTrace();
        }
    }

    //設定回傳給設備的角度方向
    private static int sensorToDeviceRotation(CameraCharacteristics cameraCharacteristics, int deviceOrientation)
    {
        int sensorOrientation = cameraCharacteristics.get(CameraCharacteristics.SENSOR_ORIENTATION);
        deviceOrientation = ORIENTATIONS.get(deviceOrientation);
        return (sensorOrientation + deviceOrientation + 360) % 360;
    }

    //設定preview的size
    private static Size chooseOptimalSize(Size[] choices, int width, int height)
    {
        List<Size> bigEnough = new ArrayList<Size>();
        for (Size option : choices)
        {
            if (option.getHeight() == option.getWidth() * height / width && option.getWidth() >= width && option.getHeight() >= height)
            {
                bigEnough.add(option);
            }
        }
        if (bigEnough.size() > 0)
        {
            return Collections.min(bigEnough, new CompareSizeByArea());
        }
        else
        {
            return choices[0];
        }
    }

    //新增錄影的資料夾
    private void createVideoFolder()
    {
        //新增錄影的資料夾
        File movieFile = getExternalFilesDir(null);
        mVideoFolder = new File(movieFile, "eye_video");
        if (!mVideoFolder.exists())
        {
            //在手機內存放影片的資料夾內新增一個目錄
            mVideoFolder.mkdirs();
        }
    }

    //設定錄影影片的詳細訊息
    private File createVideoFileName() throws IOException
    {

        //設定時間戳記
        String timestamp = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date());
        //設定影片的名稱
        String prepend = myText + "eyecapture_" + timestamp + "_";
        //設定影片的檔案類型以及位置
        File videoFile = File.createTempFile(prepend, ".mp4", mVideoFolder);
        //將videoFile設定為絕對路徑
        mVideoFileName = videoFile.getAbsolutePath();
        return videoFile;
    }

    //檢查手機版本以及權限是否符合此App
    @RequiresApi(api = Build.VERSION_CODES.M)

    private void checkWriteStoragePermission()
    {
        //確認SDK的版本，不符合則繼續執行
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M)
        {
            //調整SDK版本
            if (ContextCompat.checkSelfPermission(this, Manifest.permission.WRITE_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED)
            {
                mIsRecording = true;
                //變換錄影時的Icon
                mRecordImageButton.setImageResource(R.drawable.record);

                try
                {
                    createVideoFileName();
                    setupMediaRecorder();
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                }

                //開始錄影
                mMediaRecorder.start();

            }
            else
            {
                if (shouldShowRequestPermissionRationale(Manifest.permission.WRITE_EXTERNAL_STORAGE))
                {
                    Toast.makeText(this, "App需要儲存影片", Toast.LENGTH_SHORT).show();
                }
                requestPermissions(new String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE}, REQUEST_WRITE_EXTERNAL_STORAGE_PERMISSION_RESULT);
            }
        }
        //確認SDK的版本，符合則繼續執行
        else
        {
            mIsRecording = true;
            //變換錄影時的Icon
            mRecordImageButton.setImageResource(R.drawable.record);
            try
            {
                createVideoFileName();
                setupMediaRecorder();
            }
            catch (IOException e)
            {
                e.printStackTrace();
            }
            mMediaRecorder.start();
        }

    }

    //設定錄影時的值
    @RequiresApi(api = Build.VERSION_CODES.M)
    private void setupMediaRecorder() throws IOException
    {
        //設定來源
        mMediaRecorder.setVideoSource(MediaRecorder.VideoSource.SURFACE);
        //設定格式輸出
        mMediaRecorder.setOutputFormat(MediaRecorder.OutputFormat.MPEG_4);
        //設定錄影時的清晰度
        mMediaRecorder.setVideoEncodingBitRate(30000000);
        //設定偵數
        mMediaRecorder.setVideoFrameRate(60);
        //設定影片的Size
        mMediaRecorder.setVideoSize(mVideoSize.getWidth(), mVideoSize.getHeight());
        //設定紀錄器
        mMediaRecorder.setVideoEncoder(MediaRecorder.VideoEncoder.H264);
        //設定錄影的旋轉角度
        mMediaRecorder.setOrientationHint(mTotalRotation);
        //設定輸出檔案的名字
        mMediaRecorder.setOutputFile(mVideoFileName);
        //設定準備開始錄影
        mMediaRecorder.prepare();
        //獲得MediaRecorder的畫面
        mCaptureRequestBuilder.addTarget(recorderSurface = mMediaRecorder.getSurface());
        //重新創建錄影的畫面(同時回傳MediaRecorder以及Preview的畫面)
        Rebuild_Camera_Capture(Arrays.asList(previewSurface, recorderSurface));

    }

}

