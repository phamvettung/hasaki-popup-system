﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO
{
    internal class HasakiSystemDTO
    {
        private string ngay;
        private string maKienHang;
        private string maZone;
        private string trangThai;
        public int PickupLocationId { get; set; }
        public int ReceiverLocationId { get; set; }

        public HasakiSystemDTO()
        {
            this.ngay = string.Empty;
            this.maKienHang = string.Empty;
            this.maZone = string.Empty;
            this.trangThai = string.Empty;
            this.PickupLocationId = 0;
            this.ReceiverLocationId = 0;
        }

        public HasakiSystemDTO(string ngay, string maKienHang, string maZone, string trangThai, int pickupLocationId, int receiverLocationId)
        {
            this.ngay = ngay;
            this.maKienHang = maKienHang;
            this.maZone = maZone;
            this.trangThai = trangThai;
            this.PickupLocationId = pickupLocationId;
            this.ReceiverLocationId = receiverLocationId;
        }

        public string GetNgay() { 
            return this.ngay;
        }
        public void SetNgay(string ngay)
        {
            this.ngay = ngay;
        }
        public string GetMaKienHang() {  
            return this.maKienHang; 
        }
        public void SetMaKienHang(string maKienHang)
        {
            this.maKienHang = maKienHang;
        }
        public string GetMaZone() { 
            return this.maZone; 
        }
        public void SetMaZone(string maZone)
        {
            this.maZone = maZone;
        }
        public string GetTrangThai() { 
            return this.trangThai;
        }
        public void SetTrangThai(string trangThai)
        {
            this.trangThai = trangThai;
        }

    }
}
