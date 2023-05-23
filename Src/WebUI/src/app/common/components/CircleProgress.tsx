import React from 'react';

interface CircleProgressProps {
  progress: number; // Progress value between 0 and 100
  size?: number; // Optional size of the circle (default: 100)
  strokeWidth?: number; // Optional stroke width (default: 10)
  backgroundColor?: string; // Optional background color of the circle (default: transparent)
  progressColor?: string; // Optional color of the progress stroke (default: blue)
  textColor?: string; // Optional color of the percentage text (default: black)
  textSize?: number; // Optional font size of the percentage text (default: 16)
}

const CircleProgress: React.FC<CircleProgressProps> = ({
  progress,
  size = 100,
  strokeWidth = 10,
  backgroundColor = 'rgba(0,0,0,.1)',
  progressColor = '#21ba45',
  textColor = '#21ba45',
  textSize = 28,
}) => {
  const radius = (size - strokeWidth) / 2;
  const circumference = 2 * Math.PI * radius;
  const offset = circumference - (progress / 100) * circumference;
  const textX = size / 2;
  const textY = size / 2;

  return (
    <svg width={size} height={size}>
      <circle
        className="circle-progress"
        r={radius}
        cx={size / 2}
        cy={size / 2}
        strokeWidth={strokeWidth}
        stroke={backgroundColor}
        fill="none"
      />
      <circle
        className="circle-progress"
        r={radius}
        cx={size / 2}
        cy={size / 2}
        strokeWidth={strokeWidth}
        stroke={progressColor}
        fill="none"
        strokeDasharray={circumference}
        strokeDashoffset={offset}
      />
      <text
        x={textX}
        y={textY}
        dominantBaseline="middle"
        textAnchor="middle"
        fill={textColor}
        fontSize={textSize}
        fontWeight="bold"
      >
        {progress}%
      </text>
    </svg>
  );
};

export default CircleProgress;
