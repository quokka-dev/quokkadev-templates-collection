function New-DockerVolumeIfNotExists {
    param (
        [string]$VolumeName
    )

    # Chek if volume exists
    $existingVolume = docker volume ls -q --filter "name=^${VolumeName}$"

    # If not exists create it
    if (-not $existingVolume) {
        docker volume create $VolumeName
        Write-Output "Volume '$VolumeName' created."
    } else {
        Write-Output "Volume '$VolumeName' aleready exists."
    }
}

# Call the functions for each volume
New-DockerVolumeIfNotExists -VolumeName "quokkadevtemplates_sql_volume"
New-DockerVolumeIfNotExists -VolumeName "quokkadevtemplates_seq_volume"
New-DockerVolumeIfNotExists -VolumeName "quokkadevtemplates_key_volume"
#New-DockerVolumeIfNotExists -VolumeName "quokkadevtemplates_blob_volume"
#New-DockerVolumeIfNotExists -VolumeName "quokkadevtemplates_smtp_volume"
