module CircleCylinder
open System

let circleSurface radius = Math.PI * radius * radius

let cylinderVolumeCurry radius height =  (circleSurface radius) * height

let multiplySurfaceHeight s h = s * h

let cylinderVolumeSuperPos =  (circleSurface >> multiplySurfaceHeight)