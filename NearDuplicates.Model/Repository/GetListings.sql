select
       a.auctionid,
       a.memberId,
       m.Nickname,
       a.categoryid,
       c.mcat_path,
       a.title,
       a.BuyNowPrice,
       ad.Body,
       isnull(a.photo_id, 0) as photo_id
from dbo.auction a with (nolock)
    join dbo.member m with (nolock) on m.MemberId = a.MemberId
    join dbo.category c with (nolock) on c.CategoryId = a.CategoryId
    join dbo.auction_description ad with (nolock) on ad.auctionid = a.auctionid
where
    a.MemberId = 6139471

