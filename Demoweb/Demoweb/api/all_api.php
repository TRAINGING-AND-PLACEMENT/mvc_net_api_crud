<?php
include('connection.php');
$con = getcon();
header('Content-Type:application/json');

if($_REQUEST['what'] == 'set_user'){


    $data = json_decode(file_get_contents('php://input'), true);

    $name = $data['Name'];
    $email = $data['Email'];


    $query = "insert into aip_test (name, email) VALUES ('$name', '$email')";
    $res = mysqli_query($con,$query);

    if($res){
        // echo json_encode(['status'=>'Inserted']);
        $response['status']='inserted';
    }
    else{
        // echo json_encode(['status'=>'error']);
        $response['status']='error';
    }
}
elseif($_REQUEST['what'] == 'get_user'){

    $query = "SELECT * from aip_test";
    $res = mysqli_query($con,$query);

    $count = mysqli_num_rows($res);

    if ($count>0){
        while($row = mysqli_fetch_assoc($res)){
        $arr[] = $row;
        }
        $response = $arr;
        // echo json_encode($arr);
    }else{
        $resopnse['status'] = 'error';
    }
}
elseif($_REQUEST['what'] == 'edit_user'){

    if(isset($_GET['id'])){
        $con = getcon();
        $id = $_GET['id'];

        $query = "select * from aip_test where id = $id";
        $res = mysqli_query($con,$query);

        $count = mysqli_num_rows($res);
        if ($count>0){
            $row = mysqli_fetch_array($res);
            $response = $row;
        }else{
            $response['status'] = 'Not found';
        }
        }else{
            $response['status'] = 'Not found';
        }
}
elseif($_REQUEST['what'] == 'update_user'){
    if(isset($_GET['id'])){
        $con = getcon();
        $id = $_GET['id'];
        $data = json_decode(file_get_contents('php://input'), true);
    
        $name = $data['Name'];
        $email = $data['Email'];
    
        $query = "UPDATE aip_test SET name = '$name', email = '$email' WHERE aip_test.id = $id";
        $res = mysqli_query($con,$query);
    
        $response['status'] = 'Updated';
    }else{
        $response['status'] = 'Error';
    }
}
elseif($_REQUEST['what'] == 'delete_user'){
    if(isset($_GET['id'])){
        $con = getcon();
        $id = $_GET['id'];
    
        $query = "delete from aip_test WHERE aip_test.id = $id";
        $res = mysqli_query($con,$query);
    
        $response['status'] = 'Deleted';
    }else{
        $response['status'] = 'Error';
    }
}
echo json_encode($response); 

?>