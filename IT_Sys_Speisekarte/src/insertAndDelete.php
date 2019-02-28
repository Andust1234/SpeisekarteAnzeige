<?php

if($_SERVER['REQUEST_METHOD'] == 'POST'){

if($_POST['hidden'] == 'insert'){
    $stmtInsert = $conn->prepare("INSERT INTO test(Name) VALUES (?)");
    $stmtInsert->execute(array($_POST['name']));
}
else if($_POST['hidden'] == 'delete'){
    $sql = "DELETE FROM test WHERE ID='" . $_POST['deleteId']. "'";
    $conn->exec($sql);
}


}

?>