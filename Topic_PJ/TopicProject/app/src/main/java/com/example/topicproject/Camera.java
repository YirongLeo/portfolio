package com.example.topicproject;

import androidx.appcompat.app.AppCompatActivity;

import android.content.ContentValues;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;

public class Camera extends AppCompatActivity
{

    private static final int PERMISSION_CODE=1000;
    private static final int IMAGE_CAPTURE_CODE=1001;
    private Button button1;
    private Button button2;
    private Button button3;
    private ImageView imageView;
    private Bitmap b;
    private TextView msgReceived;
    Uri image_uri;

    public void sendImgMsg(DataOutputStream out) throws IOException
    {
        //發送圖片，將bitmap轉爲字節數
        BitmapDrawable drawable = (BitmapDrawable)imageView.getDrawable();//can work
        Bitmap bitmap = drawable.getBitmap();//can work
        //Bitmap bitmap = BitmapFactory.decodeResource(getResources(),R.drawable.ic_launcher_background);
        ByteArrayOutputStream bout = new ByteArrayOutputStream();
        b.compress(Bitmap.CompressFormat.PNG,100,bout);
        long len = bout.size();
        Log.i("sendImgMsg","len:"+len);
        out.write(bout.toByteArray());
    }

    public void takePicture()
    {
        Intent i = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        startActivityForResult(i,0);
    }

    private void openCamera()
    {
        ContentValues values = new ContentValues();
        values.put(MediaStore.Images.Media.TITLE,"New Picture");
        values.put(MediaStore.Images.Media.DESCRIPTION,"From the camera");
        image_uri = getContentResolver().insert(MediaStore.Images.Media.EXTERNAL_CONTENT_URI,values);
        //Camera intent
        Intent cameraIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        cameraIntent.putExtra(MediaStore.EXTRA_OUTPUT,image_uri);//set the image file
        startActivityForResult(cameraIntent,IMAGE_CAPTURE_CODE);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_camera);

        button1 = (Button) findViewById(R.id.Capture);//指定拍照button的id
        button2 = (Button) findViewById(R.id.ButtonSend);//指定button的id
        button3 = (Button) findViewById(R.id.Buttonback);
        imageView=(ImageView)findViewById(R.id.Picture);//指定imageView的id

        button1.setOnClickListener(new View.OnClickListener()
        { //拍照的button
            @Override
            public void onClick(View v) {
                takePicture();//original shit

                //  if (Build.VERSION.SDK_INT>=Build.VERSION_CODES.M){//uri part
                //  if(checkSelfPermission(Manifest.permission.CAMERA) ==//uri part
                //  PackageManager.PERMISSION_DENIED||//uri part
                //  checkSelfPermission(Manifest.permission.WRITE_EXTERNAL_STORAGE) ==//uri part
                //  PackageManager.PERMISSION_DENIED){//uri part
                //permission not enabled,request it
                //  String[] permission ={Manifest.permission.CAMERA,Manifest.permission.WRITE_EXTERNAL_STORAGE};//uri part
                //show popup to request permission
                // requestPermissions(permission, PERMISSION_CODE);//uri part
                //  }//uri part
                // else {//uri part
                //permission already granted
                //openCamera();//uri part
                // }//uri part
                //} //uri part
                // else{
                //openCamera();
            }
            // } //uri part
        });


        button2.setOnClickListener(new View.OnClickListener()
        {//按下去的動作的event
            @Override
            public void onClick(View v)
            { //按下去
                new Thread()
                {
                    String responce=null;
                    Socket socket;
                    String host = "192.168.0.101";
                    int post = 8004;

                    public void run()
                    {
                        try
                        {
                            socket = new Socket(host, post);
                            DataOutputStream out = new DataOutputStream(socket.getOutputStream());
                            sendImgMsg(out);//send image to socket
                            out.close();

                            socket.close();
                        } catch (Exception e)
                        {
                            e.printStackTrace();
                        }
                    }
                }.start();
            }
        });

        button3.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent intent = new Intent();
                intent.setClass(Camera.this,FuctionPage.class);
                startActivity(intent);
                Camera.this.finish();
            }
        });

    }

    @Override
    protected void onActivityResult(int requestCode,int resultCode,Intent data)
    {
        super.onActivityResult(requestCode,resultCode,data);
        if(resultCode==RESULT_OK)
        {
            b=(Bitmap)data.getExtras().get("data");
            imageView.setImageBitmap(b);
            //set the image captured to ImageView

            //imageView.setImageURI(image_uri);//uri image part
        }
    }

    //handling permission result
//   @Override
//    public void onRequestPermissionsResult(int requestCode,@NonNull String[] permissions,@NonNull int[] grantResults){
//        switch (requestCode){
//            case PERMISSION_CODE:{
//                if(grantResults.length > 0 && grantResults[0]==PackageManager.PERMISSION_GRANTED){
//                    //permission from popup was granted
//                    openCamera();
//                }
//                else{
//                    //permission from popup was denied
//                    Toast.makeText(this,"Permission denied...",Toast.LENGTH_SHORT).show();
//                    }
//                }
//            }

}