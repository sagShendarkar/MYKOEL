import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ordinalRangeDate'
})
export class OrdinalRangeDatePipe implements PipeTransform {

  transform(startDate: Date | string, endDate: Date | string): string {
    if (!startDate || !endDate) return '';

    const start = new Date(startDate);
    const end = new Date(endDate);

    const startDay = start.getDate();
    const endDay = end.getDate();
    const month = end.toLocaleString('default', { month: 'long' });
    const smonth = start.toLocaleString('default', { month: 'long' });
    const year = end.getFullYear();
    const syear = start.getFullYear();

    return `${this.getOrdinal(startDay)} ${month!==smonth?smonth:''}  ${year!==syear?syear:''} to ${this.getOrdinal(endDay)} ${month} ${year}`;
  }

  private getOrdinal(day: number): string {
    const j = day % 10;
    const k = day % 100;
    if (j === 1 && k !== 11) {
      return day + 'st';
    }
    if (j === 2 && k !== 12) {
      return day + 'nd';
    }
    if (j === 3 && k !== 13) {
      return day + 'rd';
    }
    return day + 'th';
  }
}
