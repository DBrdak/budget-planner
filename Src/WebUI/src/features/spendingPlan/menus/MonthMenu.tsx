import React, { useState } from 'react';
import { Menu, Icon } from 'semantic-ui-react';

interface MonthMenuProps {
  date: Date,
  onChange: (monthIndex: number) => void;
}

const MonthMenu = ({date, onChange}: MonthMenuProps) => {
  const months = [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December'
  ];

  const [activeMonth, setActiveMonth] = useState(date.getMonth());
  const [startIndex, setStartIndex] = useState(activeMonth-1);
  const [slideDirection, setSlideDirection] = useState<'left' | 'right' | ''>('');

  const handleNext = () => {
    if (startIndex + 3 < months.length) {
      setSlideDirection('right');
      setTimeout(() => {
        setStartIndex(startIndex + 1);
        setSlideDirection('');
      }, 300);
    }
  };

  const handlePrevious = () => {
    if (startIndex > 0) {
      setSlideDirection('left');
      setTimeout(() => {
        setStartIndex(startIndex - 1);
        setSlideDirection('');
      }, 300);
    }
  };

  const handleItemClick = (monthName: string) => {
    const monthIndex = months.findIndex(m => m === monthName)
    setActiveMonth(monthIndex)
    onChange(monthIndex)
  };

  return (
    <Menu pagination borderless style={{textAlign: 'center', border: 'none', boxShadow: 'none', width: '500px', height: '25px'}}>
      <Menu.Item disabled={startIndex === 0} onClick={handlePrevious}>
        <Icon name="chevron circle left" color={startIndex === 0 ? 'grey' : 'green'} size='big' />
      </Menu.Item>

      <Menu.Menu style={{width: '400px', justifyContent: 'center'}}>
        {months.slice(startIndex, startIndex+3).map((month, index) => (
          <Menu.Item
            key={index}
            name={month}
            active={month === months[activeMonth]}
            onClick={(e, d) => handleItemClick(d.name!)}
            color='green'
            style={{ fontSize: month === months[activeMonth] ? '25px' : '15px', width: '33%', justifyContent: 'center'}}
          />
          
        ))}
      </Menu.Menu>

      <Menu.Item disabled={startIndex + 3 >= months.length} onClick={handleNext}>
      <Icon name="chevron circle right" color={startIndex + 3 >= months.length ? 'grey' : 'green'} size='big' />
      </Menu.Item>
    </Menu>
  );
};

export default MonthMenu;
