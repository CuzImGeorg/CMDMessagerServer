<?php 
    $conn = @fsockopen('localhost', 12000);
    $running = is_resource($conn);
    

    require_once('panel.php');
?>