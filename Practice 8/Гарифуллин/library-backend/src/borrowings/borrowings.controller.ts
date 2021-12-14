import { Controller, Get, Post, Body, Patch, Param, Delete } from '@nestjs/common';
import { BorrowingsService } from './borrowings.service';
import { CreateBorrowingDto } from './dto/create-borrowing.dto';
import { UpdateBorrowingDto } from './dto/update-borrowing.dto';

@Controller('borrowings')
export class BorrowingsController {
  constructor(private readonly borrowingsService: BorrowingsService) {}

  @Post()
  create(@Body() createBorrowingDto: CreateBorrowingDto) {
    return this.borrowingsService.create(createBorrowingDto);
  }

  @Get()
  findAll() {
    return this.borrowingsService.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.borrowingsService.findOne(+id);
  }

  @Patch(':id')
  update(@Param('id') id: string, @Body() updateBorrowingDto: UpdateBorrowingDto) {
    return this.borrowingsService.update(+id, updateBorrowingDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.borrowingsService.remove(+id);
  }
}
