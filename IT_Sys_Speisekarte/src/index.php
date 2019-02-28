<!DOCTYPE html>
<html>
<head>
    <title>Speisekarte</title>  
</head>

<?php
    require('connect.php');
    require('insertAndDelete.php');
?>

    <body>

        <?php
            $stmt = $conn->prepare("SELECT * FROM test");
            $stmt->execute();
            $result = $stmt->fetchAll();

            foreach($result as $row){
                echo $row['ID']. "   " . $row['Name'];
                echo "<form id='delete" . $row['ID']. "' action='" . htmlspecialchars($_SERVER['PHP_SELF']). "' method='post'>";
                echo "<input type='hidden' name='hidden' value='delete'>";
                echo "<input type='hidden' name='deleteId' value='" . $row['ID']. "'>";
                echo "<input type='submit' value='Delete' form='delete" . $row['ID']. "'>";
                echo "</form>";
                echo "<br>";
            }
        ?>

    <br><br><br>

    <form id="insert" action="<?php echo htmlspecialchars($_SERVER['PHP_SELF']); ?>" method="post">
        Name: <input type="text" name="name"><br>
        <input type="hidden" name="hidden" value="insert">
        <input type="submit" form="insert">
    </form>

    </body>
</html>